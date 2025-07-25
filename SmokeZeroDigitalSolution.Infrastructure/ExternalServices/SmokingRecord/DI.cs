using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.SmokingRecord
{
    public static class DI
    {
        public static IServiceCollection RegisterSmokingRecordServices(this IServiceCollection services)
        {
            services.AddScoped<ISmokingRecordRepository, SmokingRecordRepository>();
            services.AddScoped<ISmokingRecordService, SmokingRecordService>();
            return services;
        }
    }
}
