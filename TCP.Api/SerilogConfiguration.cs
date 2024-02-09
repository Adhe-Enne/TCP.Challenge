using Serilog;

namespace TCP.Api
{
    public static class SerilogConfiguration
    {
        public static void UseSerilogFromSettings(this ConfigureHostBuilder Host)
        {
            string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env ?? "Production"}.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(configuration)
              .Enrich.WithThreadId()
              .CreateLogger();

            Host.UseSerilog();
        }
    }
}