using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
     public class FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork) : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository = feedbackRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<FeedbackResponseDto> CreateFeedbackAsync(CreateFeedbackDto data, CancellationToken cancellationToken)
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
                await _feedbackRepository.AddAsync(feedback);
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
        public async Task<FeedbackResponseDto?> GetByIdAsync(Guid id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            return feedback == null ? null : MapToDto(feedback);
        }

        public async Task<IEnumerable<FeedbackResponseDto>> GetByCoachIdAsync(Guid coachId)
        {
            var feedbacks = await _feedbackRepository.GetByCoachIdAsync(coachId);
            return feedbacks.Select(MapToDto);
        }

        public async Task<IEnumerable<FeedbackResponseDto>> GetByUserIdAsync(Guid userId)
        {
            var feedbacks = await _feedbackRepository.GetByUserIdAsync(userId);
            return feedbacks.Select(MapToDto);
        }


        public async Task<IEnumerable<FeedbackResponseDto>> GetByConditionAsync(Expression<Func<Feedback, bool>> expression)
        {
            var feedbacks = await _feedbackRepository.GetByConditionAsync(expression);
            return feedbacks.Select(MapToDto);
        }

        private static FeedbackResponseDto MapToDto(Feedback feedback)
        {
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
    }
}
