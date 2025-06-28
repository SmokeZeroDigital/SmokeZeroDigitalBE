namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth
{
    public class RegisterUserDto
    {
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string ConfirmPassword { get; init; } = string.Empty;
        public string FullName { get; init; } = string.Empty;
        public DateTime? DateOfBirth { get; init; }
        public GenderType Gender { get; init; } = GenderType.Unknown;

    }
}
