using SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetByPostIdAsync(Guid postId);
        Task<IEnumerable<CommentDto>> GetByArticleIdAsync(Guid articleId);
        Task<IEnumerable<CommentDto>> GetRepliesAsync(Guid parentCommentId);
        Task<Comment> GetByIdAsync(Guid id);
        Task<CommentDto> UpdateAsync(UpdateCommentDto data);
        Task<CommentDto> AddAsync(CreateCommentDto data);
        Task<bool> DeleteAsync(Guid id);
    }
}
