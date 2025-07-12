namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string body);
    }

}
