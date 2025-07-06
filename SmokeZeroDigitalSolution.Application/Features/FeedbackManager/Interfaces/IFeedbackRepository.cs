using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Interfaces
{
    public interface IFeedbackRepository : IBaseRepository<Feedback, Guid>
    {
        Task<FeedbackResponseDto> CreateFeedbackAsync(CreateFeedbackDto dto);
        Task<Feedback?> GetByIdAsync(Guid id);
        Task<IEnumerable<Feedback>> GetByCoachIdAsync(Guid coachId);
    }
}
