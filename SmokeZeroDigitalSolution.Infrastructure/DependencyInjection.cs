
using SmokeZeroDigitalSolution.Application.Common.IPersistence;
using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Identity;
using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.JWT;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Common;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Data;

namespace eCommerce_BE.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDataAccess(configuration);
            services.RegisterToken(configuration);
            services.RegisterSecurityManager(configuration);
            return services;
        }
    }
}
