using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Queries
{
    public class FeedbackQueryByUser : IRequest<QueryResult<IEnumerable<FeedbackResponseDto>>>
    {
        public Guid UserId { get; set; }
    }
}
