namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs
{
    public class FeedbackResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CoachId { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime FeedbackDate { get; set; }
    }
}
