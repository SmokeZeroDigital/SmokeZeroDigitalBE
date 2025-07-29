namespace SmokeZeroDigitalSolution.Contracts.Auth;

public class ConfirmEmailRequest
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = null!;
}
