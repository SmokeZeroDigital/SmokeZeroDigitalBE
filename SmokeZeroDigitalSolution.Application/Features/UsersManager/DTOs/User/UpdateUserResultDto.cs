namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.User
{
    public class UpdateUserResultDto
    {
        public Guid? UserId { get; init; }
        public string? Email { get; init; }
        [JsonConverter(typeof(SimpleNullableDateOnlyConverter))]
        public DateTime? DateOfBirth { get; init; }
        public string? FullName { get; init; }
        public bool? EmailConfirmed { get; init; }
        public Guid? PlanId { get; init; }
        [JsonConverter(typeof(SimpleNullableDateOnlyConverter))]
        public DateTime? LastModifiedAt { get; init; }
        public bool? IsDeleted { get; init; }
    }
}
