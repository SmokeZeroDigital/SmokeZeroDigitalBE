namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.Plan
{
    public class CreatePlanResultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public bool IsActive { get; set; } = true;
        [JsonConverter(typeof(SimpleNonNullableDateOnlyConverter))]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
