namespace SmokeZeroDigitalSolution.Contracts.Feedback
{
    public class CreateFeedbackRequest
    {
        public Guid UserId { get; set; }
        public Guid CoachId { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Rating { get; set; }
    }
}
