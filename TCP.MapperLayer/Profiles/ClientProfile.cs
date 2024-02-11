using AutoMapper;
using TCP.Model.Dto;
using TCP.Model.Entities;

namespace TCP.MapperLayer.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientDto, Client>()
                .ReverseMap();
            
            CreateMap<ClientCreateDto, Client>()
                .ReverseMap();
        }
    }
}
