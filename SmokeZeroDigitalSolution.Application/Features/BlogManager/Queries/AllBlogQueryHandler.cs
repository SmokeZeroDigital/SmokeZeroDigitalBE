using SmokeZeroDigitalSolution.Application.Features.BlogManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Queries
{
    public class AllBlogQueryHandler : IRequestHandler<AllBlogQuery, QueryResult<IQueryable<BlogArticle>>>
    {
        private readonly IBlogService _blogService;
        public AllBlogQueryHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<QueryResult<IQueryable<BlogArticle>>> Handle(AllBlogQuery request, CancellationToken cancellationToken)
        {
            try 
            {    
                var result = await _blogService.GetAllAsync();
                return QueryResult<IQueryable<BlogArticle>>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<IQueryable<BlogArticle>>.Failure(ex.Message);

            }
        }
    }
}
