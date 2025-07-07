using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.User;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Interfaces
{
    public interface IAuthService
    {

        Task<AuthResponseDto> LoginAsync(string username, string password, CancellationToken cancellationToken = default);
        Task<RegisterResultDto> RegisterAsync(
          RegisterUserDto registerUsertDto,
            CancellationToken cancellationToken = default
        );
      Task<UpdateUserResultDto> UpdateUserAsync(
        UpdateUserDto updateUserDto,
        CancellationToken cancellationToken = default
        );
        Task<AuthResponseDto> GoogleLoginAsync(AppUser user, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid userId, CancellationToken cancellationToken = default);
    }
} 