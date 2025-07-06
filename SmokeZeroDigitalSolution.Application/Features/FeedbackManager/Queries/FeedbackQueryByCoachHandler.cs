using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Queries
{
    public class FeedbackQueryByCoachHandler : IRequestHandler<FeedbackQueryByCoach, QueryResult<IEnumerable<FeedbackResponseDto>>>
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackQueryByCoachHandler(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        public async Task<QueryResult<IEnumerable<FeedbackResponseDto>>> Handle(FeedbackQueryByCoach request, CancellationToken cancellationToken)
        {
            try
            {
                var feedbacks = await _feedbackService.GetByCoachIdAsync(request.CoachId);
                return QueryResult<IEnumerable<FeedbackResponseDto>>.Success(feedbacks);
            }
            catch (Exception ex)
            {
                return QueryResult<IEnumerable<FeedbackResponseDto>>.Failure(ex.Message);
            }
        }
    }
}
