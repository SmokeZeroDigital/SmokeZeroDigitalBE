using SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Interfaces
{
    public interface ICommentRepository : IBaseRepository<Comment, Guid>
    {
        // Specialized Query Methods
        Task<IEnumerable<Comment>> GetByPostIdAsync(Guid postId);
        Task<IEnumerable<Comment>> GetByArticleIdAsync(Guid articleId);
        Task<IEnumerable<Comment>> GetRepliesAsync(Guid parentCommentId);
        Task<CommentDto> CreateCommentAsync(CreateCommentDto dto);
        Task<CommentDto> UpdateCommentAsync(UpdateCommentDto dto);
        Task<bool> DeleteAsync (Guid id);
    }
}
