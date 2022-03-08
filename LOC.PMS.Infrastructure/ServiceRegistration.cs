using LOC.PMS.Application;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Infrastructure.Repository;
using LOC.PMS.Model;
using Microsoft.Extensions.DependencyInjection;

namespace LOC.PMS.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPalletDetailsProvider, PalletDetailsProvider>();
            services.AddTransient<IPalletRepository, PalletRepository>();

        }
    }
}
