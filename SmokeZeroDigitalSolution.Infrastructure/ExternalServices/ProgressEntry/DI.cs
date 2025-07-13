using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.ProgressEntry
{
    public static class DI
    {
        public static IServiceCollection RegisterProgressEntryServices(this IServiceCollection services)
        {
            services.AddScoped<IProgressEntryRepository, ProgressEntryRepository>();
            services.AddScoped<IProgressEntryService, ProgresEntryService>();
            return services;
        }
    }
}
