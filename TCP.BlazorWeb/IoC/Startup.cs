using MudBlazor;
using MudBlazor.Services;
using TCP.BlazorWeb.Interfaces;
using TCP.BlazorWeb.Services;

namespace TCP.Api.IoC
{
    public static class Startup
    {
        public static void AddFrontTools(this IServiceCollection services)
        {
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

            services.AddSingleton(typeof(IClientService), typeof(ClientService));
            services.AddSingleton(typeof(IInvoiceService), typeof(InvoiceService));
        }
    }
}
