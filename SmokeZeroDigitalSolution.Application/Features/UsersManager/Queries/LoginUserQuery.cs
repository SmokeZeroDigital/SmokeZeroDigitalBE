namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries
{
    public class LoginUserQuery : IRequest<QueryResult<AuthResponseDto>>
    {
        public LoginUserDto User { get; init; } = default!;
    }
}