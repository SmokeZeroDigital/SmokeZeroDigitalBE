namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Queries
{
    public class FeedbackQueryById : IRequest<QueryResult<FeedbackResponseDto>>
    {
        public Guid Id { get; set; } 
    }
}
