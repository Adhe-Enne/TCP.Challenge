using AutoMapper;
using Core.Abstractions;
using TCP.Model.Constants;
using TCP.Model.Dto;
using TCP.Model.Entities;

namespace TCP.MapperLayer.Mapper
{
    public class InvoiceMapper : ViewMapper<InvoiceDto, Invoice>
    {
        IRepository<ListOption> _repository;
        public InvoiceMapper(IMapper mapper, IRepository<ListOption> repository ) : base(mapper)
        {
            _repository = repository;
            var dto = new Invoice();
            var dtos = new List<Invoice>();

          //  this.Map(dto);
            //Map(dtos);
        }

        public override IEnumerable<InvoiceDto> Map(IEnumerable<Invoice> entities)
        {
            List<InvoiceDto> mapperData = new List<InvoiceDto>();
            IQueryable<ListOption> listOptions = _repository.AsQueryable().Where(x => x.Status == Model.Enums.MainStatus.ACTIVE);
            ListOption? option;
            InvoiceDto? dto;

            foreach (var entity in entities)
            {
                dto = base.Map(entity);
                option = listOptions.FirstOrDefault(x => x.OptionType == KeyName.INVOICE_STATUS && x.Code == entity.InvoiceStatus.ToString());

                if (option is null) continue;

                mapperData.Add(dto);
                _mapper.Map(option, dto);

                option = listOptions.FirstOrDefault(x => x.OptionType == KeyName.PAYMENT_METHOD && x.Code == entity.PaymentMethod.ToString());

                if (option is null) continue;

                //base.Map(dto);
            }

            return mapperData;
        }
    }
}
