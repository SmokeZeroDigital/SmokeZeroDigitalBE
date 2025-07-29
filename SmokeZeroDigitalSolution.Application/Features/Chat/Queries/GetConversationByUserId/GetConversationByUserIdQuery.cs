namespace SmokeZeroDigitalSolution.Application.Features.Chat.Queries.GetConversationByUserIdQuery
{
    public class GetConversationByUserIdQuery : IRequest<QueryResult<List<ConversationDto>>>
    {
        public Guid AppUserId { get; set; }
    }
}
