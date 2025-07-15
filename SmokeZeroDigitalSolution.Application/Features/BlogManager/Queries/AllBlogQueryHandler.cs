using SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.BlogManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Queries
{
    public class AllBlogQueryHandler : IRequestHandler<AllBlogQuery, QueryResult<IQueryable<BlogReponseDto>>>
    {
        private readonly IBlogService _blogService;
        public AllBlogQueryHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<QueryResult<IQueryable<BlogReponseDto>>> Handle(AllBlogQuery request, CancellationToken cancellationToken)
        {
            try 
            {    
                var result = await _blogService.GetAllAsync();
                return QueryResult<IQueryable<BlogReponseDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<IQueryable<BlogReponseDto>>.Failure(ex.Message);

            }
        }
    }
}
