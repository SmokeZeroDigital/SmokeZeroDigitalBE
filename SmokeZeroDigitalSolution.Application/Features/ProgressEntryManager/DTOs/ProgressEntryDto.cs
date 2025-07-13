namespace SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs
{
    public class CreateProgressEntryDto
    {
        public Guid UserId { get; set; }
        public DateTime EntryDate { get; set; }
        public int CigarettesSmokedToday { get; set; }
        public decimal MoneySavedToday { get; set; }
        public string? HealthStatusNotes { get; set; }
        public int CravingLevel { get; set; }
        public string? Challenges { get; set; }
        public string? Successes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ProgressEntryDto : CreateProgressEntryDto
    {
        public Guid Id { get; set; }
    }
}
