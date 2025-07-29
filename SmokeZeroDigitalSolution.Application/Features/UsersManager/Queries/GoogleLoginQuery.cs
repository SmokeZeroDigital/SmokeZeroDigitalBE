namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries
{
    public class GoogleLoginQuery : IRequest<QueryResult<AuthResponseDto>>
    {
        public GoogleLoginDTO User { get; init; } = default!;
    }
}
