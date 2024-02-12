using Core.Abstractions;
using Core.Framework;
using Microsoft.EntityFrameworkCore;
using TCP.Business.Interfaces;
using TCP.Model.Constants;
using TCP.Model.Entities;
using TCP.Model.Enums;

namespace TCP.Business.Services
{
    public class InvoiceService : Service<Invoice>
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IValidatorStrategy<Invoice> _validator;

        public InvoiceService(
            IRepository<Invoice> invoiceRepository,
            IRepository<Client> clientRepository,
            IRepository<Product> productRepository,
            IValidatorStrategy<Invoice> validator
            ) :
            base(invoiceRepository)
        {
            _clientRepository = clientRepository;
            _productRepository = productRepository;
            _validator = validator;
        }

        public override IGenericResult Insert(Invoice entity)
        {
            IQueryable<Client> validClient = _clientRepository.AsQueryable().Where(x => x.Id == entity.ClientId);

            if (!validClient.Any())
                throw new TcpException(Messages.CLIENT_UNFOUND);

            if (validClient.Any(x=> x.Status != MainStatus.ACTIVE))
                throw new TcpException(Messages.CLIENT_UNFOUND);

            Product? prod;

            foreach (var detail in entity.Detail)
            {
                prod = _productRepository.Find(x => x.Id == detail.ProductId);

                if (prod is null)
                    throw new TcpException(Messages.PROD_UNFOUND);

                detail.UnitPrice = prod.Price;
                detail.LineAmount = detail.Qty * prod.Price;
                detail.Status = MainStatus.ACTIVE;
                Common.SetDateCreated(detail);
            }

            entity.TotalAmount = entity.Detail.Sum(x => x.LineAmount);
            entity.TotalQty = entity.Detail.Sum(x => x.Qty);
            entity.InvoiceStatus = Model.Enums.InvoiceStatus.NEW;
            entity.DueDate = DateTime.Now.AddDays(Constants.KeyBusiness.INVOICES_DUETIME);

            _validator.ValidateFields(entity);

            _repository.Insert(entity);

            return new GenericResult(Model.Constants.Messages.ENTITY_INSERTED);
        }

        public override IGenericResult Update(Invoice from)
        {
            IQueryable<Client> validClient = _clientRepository.AsQueryable().Where(x => x.Id == from.ClientId);

            if (!validClient.Any())
                throw new TcpException(Messages.CLIENT_UNFOUND);

            if (validClient.Any(x => x.Status != MainStatus.ACTIVE))
                throw new TcpException(Messages.CLIENT_UNFOUND);

            Invoice? toUpdate = this.Find(x => x.Id == from.Id, p=> p.Detail);

            if (toUpdate is null)
                throw new TcpException(Messages.INVOICE_UNFOUND);

            Product? prod;
            InvoiceDetail? detailToUpdate;

            foreach (var detail in from.Detail)
            {
                prod = _productRepository.Find(x => x.Id == detail.ProductId);

                if (prod is null)
                    throw new TcpException(Messages.PROD_UNFOUND);

                detailToUpdate = toUpdate.Detail.FirstOrDefault(x => x.Id == detail.Id);

                if (detailToUpdate is null)
                {
                    detailToUpdate = detail;
                    Common.SetDateCreated(detailToUpdate);
                    detailToUpdate = detail;

                    toUpdate.Detail.Add(detail);
                }
                else
                    detailToUpdate.DateUpdated = DateTime.Now;

                detailToUpdate.UnitPrice = prod.Price;
                detailToUpdate.LineAmount = detail.Qty * prod.Price;
                detailToUpdate.Status = MainStatus.ACTIVE;
            }

            toUpdate.TotalAmount = toUpdate.Detail.Sum(x => x.LineAmount);
            toUpdate.TotalQty = toUpdate.Detail.Sum(x => x.Qty);
            toUpdate.InvoiceStatus = from.InvoiceStatus;

            if (Common.IsExpired(toUpdate))
                toUpdate.InvoiceStatus = InvoiceStatus.EXPIRED;

            _validator.ValidateFields(toUpdate);
            _repository.Update(toUpdate);

            return new GenericResult(Model.Constants.Messages.ENTITY_UPDATED);
        }

        public override IQueryable<Invoice> AsQueryable()
        {
            return _repository.AsQueryable().Where(x => x.Status == MainStatus.ACTIVE)
                .Include(x => x.Client)
                .Include(x => x.Customer)
                .Include(x => x.Detail).ThenInclude(d => d.Product);
        }
    }
}
