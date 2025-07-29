namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Queries
{
    public class GetByPostQuery : IRequest<QueryResult<IEnumerable<CommentDto>>>, IHasId
    {
        public Guid Id { get; init; } = default!;
    }
}
