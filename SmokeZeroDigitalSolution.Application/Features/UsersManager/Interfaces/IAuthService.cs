using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;
namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Interfaces
{
    public interface IAuthService
    {

        Task<AuthResponseDto> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<RegisterResultDto> RegisterAsync(
          RegisterUserDto registerUsertDto,
            CancellationToken cancellationToken = default
        );
    }
} 