namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;

public class ConfirmEmailDto
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = null!;
}
