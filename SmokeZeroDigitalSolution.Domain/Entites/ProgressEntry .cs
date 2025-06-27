namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class ProgressEntry : BaseEntity
    {
        public Guid UserId { get; set; } // Foreign Key
        public AppUser User { get; set; } = default!; // Navigation Property

        public DateTime EntryDate { get; set; }
        public int CigarettesSmokedToday { get; set; }
        public decimal MoneySavedToday { get; set; }
        public string? HealthStatusNotes { get; set; }
        public int CravingLevel { get; set; } // 1-10
        public string? Challenges { get; set; }
        public string? Successes { get; set; }
    }
}
