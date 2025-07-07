namespace SmokeZeroDigitalSolution.Application.Features.Chat.Queries.GetMessages
{
    public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, QueryResult<ChatMessageDto>>
    {
        private readonly IChatMessageRepository _chatRepository;
        public GetMessageByIdQueryHandler(IChatMessageRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<QueryResult<ChatMessageDto>> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
        {
            var msg = await _chatRepository.GetByIdAsync(request.MessageId, cancellationToken);
            if (msg == null)
            {
                return QueryResult<ChatMessageDto>.NotFoundResult("Message not found.");
            }
            var dto = new ChatMessageDto
            {
                Id = msg.Id,
                SenderUserId = msg.SenderUserId,
                Content = msg.Content,
                CreatedAt = msg.CreatedAt
            };
            return QueryResult<ChatMessageDto>.Success(dto);
        }
    }
}
