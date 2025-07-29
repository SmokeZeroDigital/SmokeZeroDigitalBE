namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Queries
{
    public class FeedbackQueryHandler : IRequestHandler<FeedbackQueryById, QueryResult<FeedbackResponseDto>>
    {      
        private readonly IFeedbackService _feedbackService;
        public FeedbackQueryHandler(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        public async Task<QueryResult<FeedbackResponseDto>> Handle(FeedbackQueryById request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _feedbackService.GetByIdAsync(request.Id);
                return QueryResult<FeedbackResponseDto>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<FeedbackResponseDto>.Failure(ex.Message);
            }
        }
    }
    

    
}
