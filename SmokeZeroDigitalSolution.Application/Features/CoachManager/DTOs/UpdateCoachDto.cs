namespace SmokeZeroDigitalSolution.Application.Features.CoachManager.DTOs
{
    public class UpdateCoachDto
    {
        public string Bio { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public decimal Rating { get; set; }
    }
}
