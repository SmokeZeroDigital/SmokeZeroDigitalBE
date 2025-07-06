using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public class FeedbackRepository(ApplicationDbContext applicationDbContext) : BaseRepository<Feedback, Guid>(applicationDbContext), IFeedbackRepository
    {
        public async Task<FeedbackResponseDto> CreateFeedbackAsync(CreateFeedbackDto data)
        {
            if (data == null || string.IsNullOrEmpty(data.Content) || data.Rating < 1 || data.Rating > 5)
            {
                throw new ArgumentException("Invalid feedback data.");
            }

            var feedback = new Feedback
            {
                UserId = data.UserId,
                CoachId = data.CoachId,
                Content = data.Content,
                Rating = data.Rating,
                FeedbackDate = DateTime.UtcNow,
                IsDeleted = false
            };

            try
            {
                await AddAsync(feedback);
                return new FeedbackResponseDto
                {
                    Id = feedback.Id,
                    UserId = feedback.UserId,
                    CoachId = feedback.CoachId,
                    Content = feedback.Content,
                    Rating = feedback.Rating,
                    FeedbackDate = feedback.FeedbackDate
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create feedback: {ex.Message}", ex);
            }
        }
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
    }
}
