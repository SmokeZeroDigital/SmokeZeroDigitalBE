using SmokeZeroDigitalSolution.Application.Features.BlogManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Queries
{
    public class GetBlogByIdHandler : IRequestHandler<GetBlogByIdQuery, QueryResult<BlogArticle>>
    {
        private readonly IBlogService _blogService;
        public GetBlogByIdHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<QueryResult<BlogArticle>> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogService.GetByIdAsync(request.BlogId);
                return QueryResult<BlogArticle>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<BlogArticle>.Failure(ex.Message);
            }
        }
    }

}
