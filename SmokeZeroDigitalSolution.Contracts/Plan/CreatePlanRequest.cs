namespace SmokeZeroDigitalSolution.Contracts.Plan
{
    public class CreatePlanRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
    }
}
