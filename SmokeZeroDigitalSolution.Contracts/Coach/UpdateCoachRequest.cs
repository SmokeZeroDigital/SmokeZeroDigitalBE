namespace SmokeZeroDigitalSolution.Contracts.Coach
{
    public class UpdateCoachRequest
    {
        public string Bio { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public decimal Rating { get; set; }
    }
}
