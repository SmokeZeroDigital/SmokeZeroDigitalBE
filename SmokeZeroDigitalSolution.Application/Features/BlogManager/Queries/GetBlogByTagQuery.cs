namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Queries
{
    public class GetBlogByTagQuery : IRequest<QueryResult<IEnumerable<BlogArticle>>>
    {
        public string Tag { get; init; } = default!;
    }
  
}
