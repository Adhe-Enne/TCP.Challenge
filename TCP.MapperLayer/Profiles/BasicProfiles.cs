using AutoMapper;
using TCP.Model.Dto;
using TCP.Model.Entities;

namespace TCP.MapperLayer.Profiles
{
    public class BasicProfiles : Profile
    {
        public BasicProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ReverseMap();
        }
    }
}
