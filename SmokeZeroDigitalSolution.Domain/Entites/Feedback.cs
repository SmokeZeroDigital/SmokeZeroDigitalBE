namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class Feedback : BaseEntity
    {
        public Guid UserId { get; set; } // Foreign Key (Người gửi feedback)
        public AppUser User { get; set; } = default!; // Navigation Property

        public Guid TargetEntityId { get; set; } // ID của thực thể được đánh giá (ví dụ: CoachId, AppId)
        public string TargetEntityType { get; set; } = string.Empty; // Loại thực thể (e.g., "COACH", "PLATFORM")
        public int Rating { get; set; } // Điểm đánh giá (1-5)
        public string? Comment { get; set; }
        public DateTime FeedbackDate { get; set; } = DateTime.UtcNow;
    }
}
