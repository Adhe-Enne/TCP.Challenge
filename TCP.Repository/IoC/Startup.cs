using Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using TCP.Model.Entities;
using TCP.Model.ViewSp;
using TCP.Repository.Interfaces;
using TCP.Repository.Repository;

namespace TCP.Repository.IoC
{
    public static class Startup
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositorySql), typeof(RepositorySql));
            services.AddScoped(typeof(IRepository<Client>), typeof(Repository<Client>));
            services.AddScoped(typeof(IRepository<Product>), typeof(Repository<Product>));
            services.AddScoped(typeof(IRepository<ListOption>), typeof(Repository<ListOption>));
            services.AddScoped(typeof(IRepository<Customer>), typeof(Repository<Customer>));
            services.AddScoped(typeof(IRepository<Invoice>), typeof(Repository<Invoice>));
            services.AddScoped(typeof(IRepository<InvoiceDetail>), typeof(Repository<InvoiceDetail>));
        }
    }
}