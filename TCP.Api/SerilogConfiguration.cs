using Serilog;
using Serilog.Events;

namespace TCP.Api
{
    public static class SerilogConfiguration
    {
        public static void UseSerilogFromSettings(this ConfigureHostBuilder Host)
        {
            string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            /*
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env ?? "Production"}.json", optional: true)
                .Build();*/
  
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .MinimumLevel.Override("System", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("Logs/Api_FileLogger_.txt", 
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 1024000,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} | [{Level:u3}] | {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

            Host.UseSerilog();
        }
    }
}