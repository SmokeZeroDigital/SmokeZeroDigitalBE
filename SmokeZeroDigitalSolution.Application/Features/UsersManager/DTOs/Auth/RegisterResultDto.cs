namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth
{
    public class RegisterResultDto
    {
        public string Email { get; init; } = string.Empty;
        public string FullName { get; init; } = string.Empty;
        public DateTime? DateOfBirth { get; init; }
        public GenderType Gender { get; set; }
    }
}
