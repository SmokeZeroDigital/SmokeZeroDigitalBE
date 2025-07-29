namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class SmokingRecord : BaseEntity
    {
        public Guid UserId { get; set; } // Foreign Key
        public AppUser User { get; set; } = default!; // Navigation Property

        public int CigarettesSmoked { get; set; }
        public decimal CostIncurred { get; set; }
        public DateTime RecordDate { get; set; }
        public string? Notes { get; set; }
    }
}
