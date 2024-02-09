using AutoMapper;
using TCP.Model.Dto;
using TCP.Model.Entities;

namespace TCP.Api.Profiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<InvoiceDetailDto, InvoiceDetail>().ReverseMap();

            CreateMap<InvoiceDto, Invoice>()
                .ForMember(dest => dest.Detail, src => src.MapFrom(res => res.Detail))
                .ReverseMap();
        }
    }
}
