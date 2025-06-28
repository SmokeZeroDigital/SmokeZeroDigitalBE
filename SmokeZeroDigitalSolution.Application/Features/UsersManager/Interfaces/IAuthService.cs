namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Interfaces
{
    public interface IAuthService
    {

        Task<AuthResponseDto> LoginAsync(string username, string password, CancellationToken cancellationToken = default);
        Task<RegisterResultDto> RegisterAsync(
          RegisterUserDto registerUsertDto,
            CancellationToken cancellationToken = default
        );
    }
} 