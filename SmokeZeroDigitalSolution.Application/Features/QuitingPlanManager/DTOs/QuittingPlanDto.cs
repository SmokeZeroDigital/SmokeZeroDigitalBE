namespace SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.DTOs;
public class QuittingPlanDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ReasonToQuit { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }
    public string? Stages { get; set; }
    public string? CustomNotes { get; set; }
    public int InitialCigarettesPerDay { get; set; }
    public decimal InitialCostPerCigarette { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
