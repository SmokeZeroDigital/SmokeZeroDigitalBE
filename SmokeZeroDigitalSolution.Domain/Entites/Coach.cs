namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class Coach : BaseEntity
    {
        public Guid UserId { get; set; } // Foreign Key (liên kết với tài khoản người dùng)
        public AppUser User { get; set; } = default!; // Navigation Property

        public string Bio { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public decimal Rating { get; set; } // Average rating
        public bool IsAvailable { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        public ICollection<Feedback> FeedbacksGiven { get; set; } = new List<Feedback>(); // Phản hồi dành cho HLV này
    }
}
