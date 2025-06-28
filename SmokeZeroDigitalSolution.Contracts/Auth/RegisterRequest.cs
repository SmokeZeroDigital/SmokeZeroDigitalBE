namespace SmokeZeroDigitalSolution.Contracts.Auth
{
    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string ConfirmPassword { get; init; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
    }
}
