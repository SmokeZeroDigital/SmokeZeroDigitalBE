namespace SmokeZeroDigitalSolution.Contracts.Noti
{
    public class UpdateNotiRequest
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime ScheduledTime { get; set; }
        public int RecurrencePattern { get; set; } 

    }
}
