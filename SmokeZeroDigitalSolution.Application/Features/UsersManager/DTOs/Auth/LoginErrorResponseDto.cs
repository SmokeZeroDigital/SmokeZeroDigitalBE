namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;

public class LoginErrorResponseDto
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public string ErrorCode { get; set; } = string.Empty;
    public Guid? UserId { get; set; }
    public string? Email { get; set; }
}
