using SmokeZeroDigitalSolution.Application.Features.BlogManager.Interfaces;
namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Queries
{
    public class GetBlogByTagHandler : IRequestHandler<GetBlogByTagQuery, QueryResult<IEnumerable<BlogArticle>>>
    {
        private readonly IBlogService _blogService;
        public GetBlogByTagHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<QueryResult<IEnumerable<BlogArticle>>> Handle(GetBlogByTagQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogService.GetArticlesByTagAsync(request.Tag);
                return QueryResult<IEnumerable<BlogArticle>>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<IEnumerable<BlogArticle>>.Failure(ex.Message);
            }
        }
    }
}

