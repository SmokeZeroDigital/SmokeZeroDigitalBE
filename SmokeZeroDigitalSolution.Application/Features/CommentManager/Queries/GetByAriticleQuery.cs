namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Queries
{
    public class GetByAriticleQuery : IRequest<QueryResult<IEnumerable<CommentDto>>>, IHasId
    {
        public Guid Id { get; init; } = default!; // Represents the unique identifier for the article
    }
}
   
