using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TCP.DataBaseContext.IoC
{
    public static class Startup
    {
        public static void AddDatabaseContext(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<DataBaseContext>(c => c.UseSqlServer(connectionString));
        }
    }
}