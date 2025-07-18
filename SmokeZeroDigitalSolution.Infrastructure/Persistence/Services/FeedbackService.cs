namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
     public class FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork) : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository = feedbackRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<FeedbackResponseDto> CreateFeedbackAsync(CreateFeedbackDto data)
        {
            return await _feedbackRepository.CreateFeedbackAsync(data);
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

        private static FeedbackResponseDto MapToDto(Feedback feedback)
        {
            return new FeedbackResponseDto
            {
                Id = feedback.Id,
                UserName = feedback.User.FullName,
                CoachName = feedback.Coach.User.FullName,
                Content = feedback.Content,
                Rating = feedback.Rating,
                FeedbackDate = feedback.FeedbackDate
            };
        }
    }
}
