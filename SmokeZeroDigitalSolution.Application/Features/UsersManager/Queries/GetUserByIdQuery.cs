namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries
{
    public class GetUserByIdQuery : IRequest<QueryResult<AppUser>>
    {
        public Guid UserId { get; init; }
    }
}
