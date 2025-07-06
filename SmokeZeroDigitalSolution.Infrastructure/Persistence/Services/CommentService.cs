using SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.CommentManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
    public class CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork) : ICommentService
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        public async Task<IEnumerable<CommentDto>> GetByPostIdAsync(Guid postId)
        {
             var comment = await _commentRepository.GetByPostIdAsync(postId);
             return comment.Select(MapToDto);
        }
        public async Task<IEnumerable<CommentDto>> GetByArticleIdAsync(Guid articleId)
        {
            var comment = await _commentRepository.GetByArticleIdAsync(articleId);
            return comment.Select(MapToDto);
        }
        public async Task<IEnumerable<CommentDto>> GetRepliesAsync(Guid parentCommentId)
        {
            var comment = await _commentRepository.GetRepliesAsync(parentCommentId);
            return comment.Select(MapToDto);
        }
        public async Task<Comment> GetByIdAsync(Guid id)
        {
            return await _commentRepository.FindAsync(id);
        }
        public async Task<CommentDto> UpdateAsync(UpdateCommentDto data)
        {
            return await _commentRepository.UpdateCommentAsync(data);
        }

        public async Task<CommentDto> AddAsync(CreateCommentDto data)
        {
            return await _commentRepository.CreateCommentAsync(data);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _commentRepository.DeleteAsync(id);
        }
        private static CommentDto MapToDto(Comment c)
        {
            return new CommentDto
            {
                Id = c.Id,
                UserId = c.UserId,
                UserName = c.User?.UserName ?? "Unknown",
                PostId = c.PostId,
                ArticleId = c.ArticleId,
                Content = c.Content,
                CommentDate = c.CommentDate,
                ParentCommentId = c.ParentCommentId,
                IsDeleted = c.IsDeleted,
                Replies = c.Replies.Select(MapToDto).ToList()
            };
        }
    }
}
