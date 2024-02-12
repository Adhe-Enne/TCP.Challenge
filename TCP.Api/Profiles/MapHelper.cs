using AutoMapper;
using Azure;
using TCP.Business.Constants;
using TCP.Model.Dto;
using TCP.Model.Entities;

namespace TCP.Api.Profiles
{
    /// <summary>
    /// Realmente no me gusta esto pero no queria dejar un endpoint innecesariamente largo
    /// </summary>
    public static class MapHelper
    {
        public static IEnumerable<InvoiceDto> MapInvoiceDto(IEnumerable<Invoice> entities, IQueryable<ListOption> listOptions, IMapper mapper)
        {
            List<InvoiceDto> data = new ();
            ListOption? option;
            InvoiceDto? dto;

            foreach (var entity in entities)
            {
                dto = mapper.Map<InvoiceDto>(entity);
                option = listOptions.FirstOrDefault(x => x.OptionType == KeyBusiness.INVOICE_STATUS && x.Code == entity.InvoiceStatus.ToString());

                if (option is null) continue;

                data.Add(dto);
                mapper.Map(option, dto);

                option = listOptions.FirstOrDefault(x => x.OptionType == KeyBusiness.PAYMENT_METHOD && x.Code == entity.PaymentMethod.ToString());

                if (option is null) continue;

                mapper.Map(option, dto);
            }

            return data;
        }
    }
}
