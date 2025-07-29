namespace SmokeZeroDigitalSolution.Application.Features.Chat.Queries.GetOrCreateConversation
{
    public class GetOrCreateConversationQuery : IRequest<QueryResult<ConversationDto>>
    {
        public Guid AppUserId { get; set; }
        public Guid CoachId { get; set; }
    }
}
