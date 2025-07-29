namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs
{
    public class FeedbackResponseDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string CoachName { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime FeedbackDate { get; set; }
    }
}
