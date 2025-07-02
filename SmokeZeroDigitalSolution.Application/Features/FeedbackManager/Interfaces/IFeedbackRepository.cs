namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Interfaces
{
    public interface IFeedbackRepository : IBaseRepository<Feedback, Guid>
    {
        Task<Feedback?> GetByIdAsync(Guid id);
        Task<IEnumerable<Feedback>> GetByCoachIdAsync(Guid coachId);
        Task<IEnumerable<Feedback>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Feedback>> GetByConditionAsync(Expression<Func<Feedback, bool>> expression);
    }
}
