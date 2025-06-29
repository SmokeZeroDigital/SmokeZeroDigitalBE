namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class Conversation : BaseEntity
    {
        public Guid CoachId { get; set; } // Coach tham gia hội thoại
        public Coach Coach { get; set; } = default!;

        public Guid UserId { get; set; } // User tham gia hội thoại
        public AppUser User { get; set; } = default!;

        public string? LastMessage { get; set; } // Nội dung tin nhắn cuối cùng
        public string? LastMessageSender { get; set; } // Ai gửi tin nhắn cuối cùng (User/Coach)
        public bool IsActive { get; set; } = true;

        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }
}
