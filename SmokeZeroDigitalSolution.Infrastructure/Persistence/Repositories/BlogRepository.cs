using SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.BlogManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public class BlogRepository(ApplicationDbContext applicationDbContext) : BaseRepository<BlogArticle, Guid>(applicationDbContext), IBlogRepository
    {
        public async Task<BlogArticle> CreateBlogAsync(CreateBlogDto dto)
        {
            var blogArticle = new BlogArticle
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Content = dto.Content,
                PublishDate = DateTime.UtcNow,
                ViewCount = 0,
                AuthorUserId = dto.AuthorUserId,
                Tags = dto.Tags,
                CreatedAt = DateTime.UtcNow
            };
            await AddAsync(blogArticle);
            return blogArticle;
        }
        public async Task<BlogArticle> UpdateBlogAsync(UpdateBlogDto dto)
        {
            var blogArticle = await FindAsync(dto.Id);
            if (blogArticle == null) throw new KeyNotFoundException("Blog article not found.");
            blogArticle.Title = dto.Title;
            blogArticle.Content = dto.Content;
            blogArticle.Tags = dto.Tags.ToLower();
            blogArticle.LastModifiedAt = DateTime.UtcNow;
            Update(blogArticle);
            return blogArticle;
        }
        public async Task<IEnumerable<BlogArticle>> GetArticlesByTagAsync(string tag)
        {
            var tagL = tag.ToLower();
            return await Get(a => a.Tags.ToLower().Contains(tagL))
                .Include(a => a.AuthorUser)
                .ToListAsync();
        }
        public async Task<int> IncreaseViewCountAsync(Guid articleId)
        {
            var article = await FindAsync(articleId);
            if (article != null)
            {
                article.ViewCount++;
            }
            return article?.ViewCount ?? 0;
        }
    }
}
