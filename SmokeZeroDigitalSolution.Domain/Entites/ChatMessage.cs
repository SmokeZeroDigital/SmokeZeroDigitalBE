namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class ChatMessage : BaseEntity
    {
        public Guid ConversationId { get; set; } // Foreign Key tới Conversation
        public Conversation Conversation { get; set; } = default!;

        public Guid SenderUserId { get; set; } // Người gửi (User hoặc Coach)
        public AppUser User { get; set; } = default!;

        public Guid? CoachId { get; set; } // Nếu là Coach gửi
        public Coach? Coach { get; set; }

        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? MessageType { get; set; } // TEXT, IMAGE, VIDEO, FILE
        public bool IsRead { get; set; } = false;
    }
}
