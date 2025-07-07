namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.Plan
{
    public class GetPlanResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public bool IsActive { get; set; } = true;
        [JsonConverter(typeof(SimpleNullableDateOnlyConverter))]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
