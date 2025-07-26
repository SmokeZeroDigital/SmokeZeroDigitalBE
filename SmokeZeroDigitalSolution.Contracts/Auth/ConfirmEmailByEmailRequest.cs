namespace SmokeZeroDigitalSolution.Contracts.Auth;

public class ConfirmEmailByEmailRequest
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}
