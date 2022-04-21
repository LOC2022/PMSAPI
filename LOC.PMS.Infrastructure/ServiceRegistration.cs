using System.Reflection;
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
            services.AddTransient<IPalletDetailsRepository, PalletDetailsRepository>();
            services.AddTransient<IMembershipProvider, MembershipsProvider>();
            services.AddTransient<IMembershipRepository, MembershipRepository>();
            services.AddTransient<IVendorDetailsProvider, VendorDetailsProvider>();
            services.AddTransient<IVendorDetailsRepository, VendorDetailsRepository>();
            services.AddTransient<IOrdesDetailProvider, OrderDetailsProvider>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ITransactionDetailsProvider, TransactionDetailsProvider>();            
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IReportDetailsProvider, ReportDetailsProvider>();
            services.AddTransient<IReportDetailsRepository, ReportDetailsRepository>();
            services.AddTransient<IContext, Context>();
            services.AddFluentValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddSingleton(Log.Logger);
        }
    }
}
