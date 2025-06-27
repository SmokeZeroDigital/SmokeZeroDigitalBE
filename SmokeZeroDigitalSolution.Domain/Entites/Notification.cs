namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; } // Foreign Key
        public AppUser User { get; set; } = default!; // Navigation Property
        public string Type { get; set; } = string.Empty; // e.g., "Motivation", "Reminder", "Achievement"
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime ScheduledTime { get; set; }
        public DateTime? SentTime { get; set; }
        public bool IsRead { get; set; }
    }
}
