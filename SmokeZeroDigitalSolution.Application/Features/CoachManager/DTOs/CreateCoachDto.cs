namespace SmokeZeroDigitalSolution.Application.Features.CoachManager.DTOs
{
    public class CreateCoachDto
    {
        public Guid UserId { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public decimal Rating { get; set; }
    }
}
