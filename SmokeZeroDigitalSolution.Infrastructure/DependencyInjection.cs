using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Interfaces;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories;

namespace SmokeZeroDigitalSolution.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDataAccess(configuration);
            services.RegisterToken(configuration);
            services.RegisterSecurityManager(configuration);
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            return services;
        }
    }
}
