namespace SmokeZeroDigitalSolution.Application.Features.Chat.Queries
{
    public class GetUsersByCoachIdHandler : IRequestHandler<GetUsersByCoachIdQuery, QueryResult<List<UserInfoDto>>>
    {
        private readonly IConversationService _conversationService;
        public GetUsersByCoachIdHandler(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }
        public async Task<QueryResult<List<UserInfoDto>>> Handle(GetUsersByCoachIdQuery request, CancellationToken cancellationToken)
        {   
            try
            {
                var result = await _conversationService.GetUsersByCoachAsync(request.CoachId);
                return QueryResult<List<UserInfoDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<List<UserInfoDto>>.Failure(ex.Message);
            }
        }
    }
   
}
