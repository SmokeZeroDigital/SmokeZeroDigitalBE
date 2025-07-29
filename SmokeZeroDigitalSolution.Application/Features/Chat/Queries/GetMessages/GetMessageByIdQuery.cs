namespace SmokeZeroDigitalSolution.Application.Features.Chat.Queries.GetMessages
{
    public class GetMessageByIdQuery : IRequest<QueryResult<ChatMessageDto>>
    {
        public Guid MessageId { get; set; }

    }
}
