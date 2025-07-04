using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Chat;
using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Payment;

namespace SmokeZeroDigitalSolution.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDataAccess(configuration);
            services.RegisterToken(configuration);
            services.RegisterSecurityManager(configuration);
            services.RegisterPaymentPlan(configuration);
            services.RegisterChatRealTime();
            return services;
        }
    }
}
