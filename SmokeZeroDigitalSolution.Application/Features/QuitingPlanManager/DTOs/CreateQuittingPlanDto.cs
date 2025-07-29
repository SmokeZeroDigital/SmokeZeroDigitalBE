namespace SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.DTOs
{
    public class CreateQuittingPlanDto
    {
        public Guid UserId { get; set; }
        public string ReasonToQuit { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public string? Stages { get; set; }
        public string? CustomNotes { get; set; }
        public int InitialCigarettesPerDay { get; set; }
        public decimal InitialCostPerCigarette { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
