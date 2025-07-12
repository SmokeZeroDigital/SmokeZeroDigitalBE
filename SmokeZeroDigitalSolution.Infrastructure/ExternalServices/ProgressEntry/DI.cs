namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.ProgressEntry
{
    public static class DI
    {
        public static IServiceCollection RegisterProgressEntryServices(this IServiceCollection services)
        {
            //services.AddScoped<IProgressEntryRepository, ProgressEntryRepository>();
            //services.AddScoped<IProgressEntryService, ProgressEntryService>();
            return services;
        }
    }
}
