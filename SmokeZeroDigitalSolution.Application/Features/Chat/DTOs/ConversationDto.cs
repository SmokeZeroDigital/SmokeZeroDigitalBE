namespace SmokeZeroDigitalSolution.Application.Features.Chat.DTOs
{
    public class ConversationDto
    {
        public Guid Id { get; set; }
        public Guid CoachId { get; set; }
        public string CoachName { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public string? LastMessage { get; set; }
        public string? LastMessageSender { get; set; }
        public bool IsActive { get; set; }
    }
}
