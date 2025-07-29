using SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.QuittingPlan
{
    public static class DI
    {
        public static IServiceCollection RegisterQuittingPlanServices(this IServiceCollection services)
        {
            services.AddScoped<IQuittingPlanRepository, QuittingPlanRepository>();
            services.AddScoped<IQuittingPlanService, QuittingPlanService>();
            return services;
        }
    }
}
