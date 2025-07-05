namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs
{
    public class CreateFeedbackDto
    {
        public Guid UserId { get; set; }
        public Guid CoachId { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Rating { get; set; }
    }
}
