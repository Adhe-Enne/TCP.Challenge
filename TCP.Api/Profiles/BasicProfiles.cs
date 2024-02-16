using AutoMapper;
using TCP.Model.Dto;
using TCP.Model.Entities;

namespace TCP.Api.Profiles
{
    public class BasicProfiles : Profile
    {
        public BasicProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ReverseMap();

            CreateMap<ListOption, ListOptionDto>()
                .ReverseMap();

            CreateMap<Customer, CustomerDto>()
                .ReverseMap();
        }
    }
}
