using Microsoft.Extensions.Hosting;
using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _provider;

        public NotificationBackgroundService(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _provider.CreateScope();
                var sender = scope.ServiceProvider.GetRequiredService<INotiService>();
                await sender.SendPendingNotificationsAsync();

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }

}
