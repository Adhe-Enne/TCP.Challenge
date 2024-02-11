
using Core.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TCP.Business.Interfaces;
using TCP.Business.Services;
using TCP.Business.Strategy;
using TCP.Business.Validators;
using TCP.Model.Entities;

namespace TCP.Business.IoC
{
    public static class Startup
    {
        public static void AddBusiness(this IServiceCollection services)
        {
            //Validators 
            services.AddScoped<IValidator<Client>, ClientValidator>();
            services.AddScoped<IValidator<Invoice>, InvoiceValidator>();

            //BusinessIService
            services.AddScoped(typeof(IValidatorStrategy<Client>), typeof(ValidatorStrategy<Client>));
            services.AddScoped(typeof(IValidatorStrategy<Invoice>), typeof(ValidatorStrategy<Invoice>));
            services.AddScoped(typeof(IService<Invoice>), typeof(InvoiceService));
            services.AddScoped(typeof(IService<Client>), typeof(ClientService));
            services.AddScoped(typeof(IService<InvoiceDetail>), typeof(Service<InvoiceDetail>));
            services.AddScoped(typeof(IService<Product>), typeof(Service<Product>));
            services.AddScoped(typeof(IService<ListOption>), typeof(Service<ListOption>));

        }
    }
}