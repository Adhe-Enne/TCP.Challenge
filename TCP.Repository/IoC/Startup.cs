using Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using TCP.Model.Entities;

namespace TCP.Repository.IoC
{
    public static class Startup
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<Client>), typeof(Repository<Client>));
            services.AddScoped(typeof(IRepository<Product>), typeof(Repository<Product>));
            services.AddScoped(typeof(IRepository<ListOption>), typeof(Repository<ListOption>));
            services.AddScoped(typeof(IRepository<Invoice>), typeof(Repository<Invoice>));
            services.AddScoped(typeof(IRepository<InvoiceDetail>), typeof(Repository<InvoiceDetail>));
        }
    }
}