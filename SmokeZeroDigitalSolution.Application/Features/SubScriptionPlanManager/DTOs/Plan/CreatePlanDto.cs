namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.Plan
{
    public class CreatePlanDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; } 
    }
}
