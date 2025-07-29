namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class Feedback : BaseEntity
    {
        public Guid UserId { get; set; } // Người gửi feedback
        public AppUser User { get; set; } = default!;

        public Guid CoachId { get; set; } // Đối tượng nhận feedback
        public Coach Coach { get; set; } = default!;

        public string Content { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime FeedbackDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}
