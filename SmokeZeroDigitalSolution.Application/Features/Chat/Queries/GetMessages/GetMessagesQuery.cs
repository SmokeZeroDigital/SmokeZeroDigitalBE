namespace SmokeZeroDigitalSolution.Application.Features.Chat.Queries.GetMessages
{
    public class GetMessagesQuery : IRequest<QueryResult<List<ChatMessageDto>>>
    {
        public Guid ConversationId { get; set; }
    }
}
