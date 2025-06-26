namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class ChatMessage : BaseEntity
    {
        public Guid SenderUserId { get; set; } // Foreign Key
        public AppUser SenderUser { get; set; } = default!; // Navigation Property

        public Guid? ReceiverUserId { get; set; } // Foreign Key (nếu là chat 1-1 giữa user)
        public AppUser? ReceiverUser { get; set; } // Navigation Property

        public Guid? CoachId { get; set; } // Foreign Key (nếu là chat với HLV)
        public Coach? Coach { get; set; } // Navigation Property

        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; }
    }
}
