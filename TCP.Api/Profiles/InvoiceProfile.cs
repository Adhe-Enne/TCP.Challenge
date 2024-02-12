using AutoMapper;
using TCP.Business.Constants;
using TCP.Model.Constants;
using TCP.Model.Dto;
using TCP.Model.Entities;

namespace TCP.Api.Profiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<InvoiceDetail, InvoiceDetailUpdateDto>().ReverseMap();
            CreateMap<InvoiceDetail, InvoiceDetailCreateDto>().ReverseMap();
            CreateMap<Invoice, InvoiceUpdateDto>().ReverseMap();
            CreateMap<Invoice, InvoiceHeadDto>().ReverseMap();
            CreateMap<Invoice, InvoiceCreateDto>().ReverseMap();

            CreateMap<InvoiceDetail, InvoiceDetailDto>()
                .ForMember(dest => dest.ProductCode, src => src.MapFrom(res => res.Product.Code))
                .ForMember(dest => dest.ProductDescription, src => src.MapFrom(res => res.Product.Description))
                .ReverseMap();

            CreateMap<Invoice, InvoiceDto>()
               .ForMember(dest => dest.Detail, src => src.MapFrom(res => res.Detail))
                .ForMember(dest => dest.ClientName, src => src.MapFrom(res => res.Client.CompanyName))
                .ForMember(dest => dest.ClientAdress, src => src.MapFrom(res => res.Client.Adress))
                .ForMember(dest => dest.ClientEmail, src => src.MapFrom(res => res.Client.Email))
                .ForMember(dest => dest.ClientPhone, src => src.MapFrom(res => res.Client.Phone))
                .ForMember(dest => dest.CompanyName, src => src.MapFrom(res => res.Customer.Name))
                .ForMember(dest => dest.CompanyAdress, src => src.MapFrom(res => res.Client.Adress))
                .ForMember(dest => dest.CompanyPhone, src => src.MapFrom(res => res.Client.Phone))
                .ForMember(dest => dest.CompanyEmail, src => src.MapFrom(res => res.Client.Email))
                .ForMember(dest => dest.TotalQty, src => src.MapFrom(res => res.Detail.Sum(x => x.Qty)))
                .ForMember(dest => dest.TotalAmount, src => src.MapFrom(res => res.Detail.Sum(x => x.UnitPrice * x.Qty)))
                .ForMember(dest => dest.StatusCode, src => src.MapFrom(res => res.Status))
                .ForMember(dest => dest.PaymentCode, src => src.MapFrom(res => res.PaymentMethod))
                .ReverseMap();

            CreateMap<ListOption, InvoiceDto>()
            //    .ForMember(dest => dest.sta)
                .ForMember(dest => dest.InvoiceStatus, opt =>
                {
                    opt.PreCondition(s => s.OptionType.Equals(KeyBusiness.INVOICE_STATUS));
                    opt.MapFrom(src => src.Name);
                })
                .ForMember(dest => dest.StatusDescription, opt =>
                {
                    opt.PreCondition(s => s.OptionType.Equals(KeyBusiness.INVOICE_STATUS));
                    opt.MapFrom(src => src.Description);
                })
                .ForMember(dest => dest.PaymentMethod, opt =>
                {
                    opt.PreCondition(s => s.OptionType.Equals(KeyBusiness.PAYMENT_METHOD));
                    opt.MapFrom(src => src.Name);
                })
                .ForMember(dest => dest.PaymentDescription, opt =>
                {
                    opt.PreCondition(s => s.OptionType.Equals(KeyBusiness.PAYMENT_METHOD));
                    opt.MapFrom(src => src.Description);
                });
        }
    }
}
