using Core.Abstractions;
using Core.Framework;
using FluentValidation;
using TCP.Model.Constants;
using TCP.Model.Entities;
using TCP.Model.Enums;

namespace TCP.Business.Services
{
    public class InvoiceService : Service<Invoice>, IServiceCrud<Invoice>
    {
        IRepository<Client> _clientRepository;
        IRepository<Product> _productRepository;

        public InvoiceService(
            IRepository<Invoice> invoiceRepository,
            IRepository<Client> clientRepository,
            IRepository<Product> productRepository,
            IValidator<Invoice> validator) :
            base(invoiceRepository, validator)
        {
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }

        public IGenericResult Insert(Invoice entity)
        {
            bool client = _clientRepository.AsQueryable().Any(x => x.Id == entity.ClientId);

            if (!client)
                throw new TcpException(Messages.CLIENT_UNFOUND);

            Product? prod;

            foreach(var detail in entity.Detail)
            {
                prod = _productRepository.Find(x => x.Id == detail.ProductId);

                if (prod is null)
                    throw new TcpException(Messages.PROD_UNFOUND);

                detail.UnitPrice = prod.Price;
                detail.LineAmount = detail.Qty * prod.Price;
                detail.Status = MainStatus.ACTIVE;
            }

            entity.TotalAmount = entity.Detail.Sum(x => x.LineAmount);
            entity.TotalQty = entity.Detail.Sum(x => x.Qty);
            entity.InvoiceStatus = Model.Enums.InvoiceStatus.NEW;

            Validate(entity);

            _repository.Insert(entity);

            return new GenericResult(Model.Constants.Messages.ENTITY_INSERTED);
        }

        public IGenericResult Update(Invoice from)
        {
            Invoice? toUpdate = this.Find(x => x.Id == from.Id);

            bool client = _clientRepository.AsQueryable().Any(x => x.Id == from.ClientId);

            if (!client)
                throw new TcpException(Messages.CLIENT_UNFOUND);

            Product? prod;
            InvoiceDetail? detailToUpdate;

            foreach (var detail in from.Detail)
            {
                prod = _productRepository.Find(x => x.Id == detail.ProductId);

                if (prod is null)
                    throw new TcpException(Messages.PROD_UNFOUND);

                detailToUpdate = toUpdate.Detail.FirstOrDefault(x => x.InvoiceId == detail.InvoiceId);

                if (detailToUpdate is null)
                {
                    detailToUpdate = detail;
                    toUpdate.Detail.Add(detail); 
                }
                else
                    detailToUpdate = detail;

                detailToUpdate.UnitPrice = prod.Price;
                detailToUpdate.LineAmount = detail.Qty * prod.Price;
                detailToUpdate.Status = MainStatus.ACTIVE;
            }

            this.Validate(toUpdate);
            _repository.Update(toUpdate);

            return new GenericResult(Model.Constants.Messages.ENTITY_UPDATED);
        }
    }
}
