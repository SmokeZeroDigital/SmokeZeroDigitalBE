namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class QuittingPlan : BaseEntity
    {
        public Guid UserId { get; set; } // Foreign Key
        public AppUser User { get; set; } = default!; // Navigation Property

        public string ReasonToQuit { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public string? Stages { get; set; } // Có thể lưu JSON hoặc TEXT
        public string? CustomNotes { get; set; }
        public int InitialCigarettesPerDay { get; set; }
        public decimal InitialCostPerCigarette { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
