namespace SmokeZeroDigitalSolution.Application.Features.Chat.DTOs
{
    public class ChatMessageDto
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid SenderUserId { get; set; }
        public Guid? CoachId { get; set; }

        public string Content { get; set; } = string.Empty;
        public string? MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; } = false;

    }
}
