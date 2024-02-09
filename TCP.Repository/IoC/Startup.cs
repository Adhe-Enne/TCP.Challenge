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
        }
    }
}