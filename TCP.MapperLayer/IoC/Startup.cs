using Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using TCP.MapperLayer.Mapper;
using TCP.MapperLayer.Profiles;
using TCP.Model.Dto;
using TCP.Model.Entities;

namespace TCP.MapperLayer.IoC
{
    public static class Startup
    {
        public static void AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BasicProfiles));
            services.AddAutoMapper(typeof(ClientProfile));
            services.AddAutoMapper(typeof(InvoiceProfile));
            services.AddScoped(typeof(IViewMapper<InvoiceDto, Invoice>), typeof(InvoiceMapper));
        }
    }
}
