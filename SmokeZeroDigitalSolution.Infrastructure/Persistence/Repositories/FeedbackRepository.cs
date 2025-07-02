using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public class FeedbackRepository(ApplicationDbContext applicationDbContext) : BaseRepository<Feedback, Guid>(applicationDbContext), IFeedbackRepository
    {
        public async Task<Feedback?> GetByIdAsync(Guid id)
        {
            var query = Get(f => f.Id == id && !f.IsDeleted);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Feedback>> GetByCoachIdAsync(Guid coachId)
        {
            var query = Get(f => f.CoachId == coachId && !f.IsDeleted);
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Feedback>> GetByUserIdAsync(Guid userId)
        {
            var query = Get(f => f.UserId == userId && !f.IsDeleted);
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Feedback>> GetByConditionAsync(Expression<Func<Feedback, bool>> expression)
        {
            return await Get(expression).Where(f => !f.IsDeleted).ToListAsync();
        }
    }
}
