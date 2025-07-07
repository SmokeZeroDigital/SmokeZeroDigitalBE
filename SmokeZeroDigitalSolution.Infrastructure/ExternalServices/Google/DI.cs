using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Google.Auth;
namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Google
{
    public static class DI
    {
        public static IServiceCollection RegisterGooglePlan(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            return services;
        }
    }
}
