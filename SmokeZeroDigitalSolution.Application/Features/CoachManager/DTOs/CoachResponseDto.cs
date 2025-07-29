namespace SmokeZeroDigitalSolution.Application.Features.CoachManager.DTOs
{
    public class CoachResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }
    }
}
