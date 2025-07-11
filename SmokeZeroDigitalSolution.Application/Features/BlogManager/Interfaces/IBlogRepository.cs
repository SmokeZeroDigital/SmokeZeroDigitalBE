using SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Interfaces
{
    public interface IBlogRepository : IBaseRepository<BlogArticle, Guid>
    {
        Task<BlogArticle> CreateBlogAsync(CreateBlogDto dto);
        Task<BlogArticle> UpdateBlogAsync(UpdateBlogDto dto);
        Task<IEnumerable<BlogArticle>> GetArticlesByTagAsync(string tag);
        Task<int> IncreaseViewCountAsync(Guid articleId);
    }
}
