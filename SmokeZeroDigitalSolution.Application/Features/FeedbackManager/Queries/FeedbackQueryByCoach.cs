using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Queries
{
    public class FeedbackQueryByCoach : IRequest<QueryResult<IEnumerable<FeedbackResponseDto>>>
    {
        public Guid CoachId { get; set; }
    }
}
