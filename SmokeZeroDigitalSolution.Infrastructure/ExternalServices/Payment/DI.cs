namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Payment
{
    public static class DI
    {
        public static IServiceCollection RegisterPaymentPlan(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IScriptionPlanRepository, ScriptionPlanRepository>();
            services.AddScoped<IScriptionPlanService, ScriptionPlanServices>();
            services.AddScoped<IVNPayService, VNPayService>();
            return services;
        }
    }
}
