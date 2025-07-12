namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.DTOs
{
    public class CreateNotiDto
    {
        public Guid UserId { get; set; }
        public string Type { get; set; } = string.Empty; 
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime ScheduledTime { get; set; }
        public int RecurrencePattern { get; set; }
    }
}
