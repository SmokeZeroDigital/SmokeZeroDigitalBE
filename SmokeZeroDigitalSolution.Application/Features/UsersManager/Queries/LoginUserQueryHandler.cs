using SmokeZeroDigitalSolution.Application.Features.UsersManager.Exceptions;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, QueryResult<AuthResponseDto>>
    {
        private readonly IAuthService _identityService;

        public LoginUserQueryHandler(IAuthService identityService)
        {
            _identityService = identityService;
        }

        public async Task<QueryResult<AuthResponseDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _identityService.LoginAsync(request.User.Username, request.User.Password, cancellationToken);
                return QueryResult<AuthResponseDto>.Success(result);
            }
            catch (UnconfirmedEmailException ex)
            {
                // Return a specific error message with additional data for unconfirmed email
                return QueryResult<AuthResponseDto>.Failure($"EMAIL_NOT_CONFIRMED:{ex.UserId}:{ex.Email}:{ex.Message}");
            }
            catch (Exception ex)
            {
                return QueryResult<AuthResponseDto>.Failure(ex.Message);
            }
        }
    }
}