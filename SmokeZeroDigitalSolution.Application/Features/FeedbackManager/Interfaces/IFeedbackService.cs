using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Commands;
using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Interfaces
{
    public interface IFeedbackService
    {
        Task<FeedbackResponseDto> CreateFeedbackAsync(CreateFeedbackDto data, CancellationToken cancellationToken);
        Task<FeedbackResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<FeedbackResponseDto>> GetByCoachIdAsync(Guid coachId);
        Task<IEnumerable<FeedbackResponseDto>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<FeedbackResponseDto>> GetByConditionAsync(Expression<Func<Feedback, bool>> expression);
    }
}
