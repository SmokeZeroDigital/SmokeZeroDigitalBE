using SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Queries
{
    public class AllBlogQuery : IRequest<QueryResult<IQueryable<BlogReponseDto>>>
    {
    }
}
