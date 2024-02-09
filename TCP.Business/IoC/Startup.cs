
using Core.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using TCP.Business.Services;
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

            //Business
            services.AddScoped(typeof(IService<Client>), typeof(ClientService));
        }
    }
}