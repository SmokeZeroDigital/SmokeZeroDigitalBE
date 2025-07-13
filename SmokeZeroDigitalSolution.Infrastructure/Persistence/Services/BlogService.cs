using SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.BlogManager.Interfaces;
using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
    public class BlogService(IBlogRepository blogRepository) : IBlogService
    {
        private readonly IBlogRepository _blogRepository = blogRepository;

        public async Task<BlogArticle> CreateAsync(CreateBlogDto dto)
        {
            return await _blogRepository.CreateBlogAsync(dto);
		}

		public async Task<bool> DeleteBlogAsync(Guid id)
		{
			return await _blogRepository.Remove(id);
		}

		public async Task<IQueryable<BlogArticle>> GetAllAsync()
        {
            return _blogRepository.GetAll().Include(x => x.AuthorUser);
        }

        public async Task<IEnumerable<BlogArticle>> GetArticlesByTagAsync(string tag)
        {
            return await _blogRepository.GetArticlesByTagAsync(tag);
        }

        public async Task<BlogArticle> GetByIdAsync(Guid id)
        {
            return await _blogRepository
                .GetAll()
                .Include(b => b.AuthorUser)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<int> IncreaseViewCountAsync(Guid articleId)
        {
            return await _blogRepository.IncreaseViewCountAsync(articleId);
        }

        public async Task<BlogArticle> UpdateAsync(UpdateBlogDto dto)
        {
            return await _blogRepository.UpdateBlogAsync(dto);
        }
    }
}
