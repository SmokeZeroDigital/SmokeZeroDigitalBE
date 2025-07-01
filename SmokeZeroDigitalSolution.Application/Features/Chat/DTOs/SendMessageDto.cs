namespace SmokeZeroDigitalSolution.Application.Features.Chat.DTOs
{
    public class SendMessageDto
    {
        public Guid ConversationId { get; init; }
        public Guid SenderUserId { get; init; }
        public Guid? CoachId { get; init; }
        public string Content { get; init; } = string.Empty;
        public string? MessageType { get; init; }
    }
}
