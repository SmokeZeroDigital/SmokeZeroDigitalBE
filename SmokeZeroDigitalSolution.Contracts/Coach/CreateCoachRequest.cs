namespace SmokeZeroDigitalSolution.Contracts.Coach
{
    public class CreateCoachRequest
    {
        public Guid UserId { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public decimal Rating { get; set; }
    }
}
