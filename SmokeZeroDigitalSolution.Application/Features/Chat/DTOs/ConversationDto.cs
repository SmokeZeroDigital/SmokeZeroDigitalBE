namespace SmokeZeroDigitalSolution.Application.Features.Chat.DTOs
{
    public class ConversationDto
    {
        public Guid Id { get; set; }
        public Guid CoachId { get; set; }
        public string CoachName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? CoachProfilePictureUrl { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public string? LastMessage { get; set; }
        public string? LastMessageSender { get; set; }
        public bool IsActive { get; set; }

    }
}
