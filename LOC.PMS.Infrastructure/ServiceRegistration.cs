﻿using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using LOC.PMS.Application;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace LOC.PMS.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPalletDetailsProvider, PalletDetailsProvider>();
            services.AddTransient<IPalletRepository, PalletRepository>();
            services.AddTransient<IContext, Context>();
            services.AddFluentValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddSingleton(Log.Logger);
        }
    }
}
