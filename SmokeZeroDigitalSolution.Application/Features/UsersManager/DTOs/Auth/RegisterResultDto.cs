namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth
{
    public class RegisterResultDto
    {
        public Guid UserId { get; init; }
        public string Email { get; init; } = string.Empty;
        public string FullName { get; init; } = string.Empty;

        [JsonConverter(typeof(SimpleNullableDateOnlyConverter))]
        public DateTime? DateOfBirth { get; init; }
        public GenderType Gender { get; set; }
        [JsonConverter(typeof(SimpleNullableDateOnlyConverter))]
        public DateTime? CreateAt { get; init; }
    }
}
