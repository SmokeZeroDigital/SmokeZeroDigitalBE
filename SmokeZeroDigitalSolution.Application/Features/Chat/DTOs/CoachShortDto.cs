namespace SmokeZeroDigitalSolution.Application.Features.Chat.DTOs
{
    public class CoachShortDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
    }
}
