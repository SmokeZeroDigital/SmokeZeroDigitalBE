using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface;

namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Notification
{
    public static class DI
    {
        public static IServiceCollection RegisterNotiFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotiRepo, NotiRepository>();
            services.AddScoped<INotiService, NotiService>();
            return services;
        }
    }
}
