namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Queries
{
    public class GetBlogByIdQuery : IRequest<QueryResult<BlogArticle>>
    {
        public Guid BlogId { get; init; } = default!;
    }
}