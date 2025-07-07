namespace SmokeZeroDigitalSolution.Application.Features.Chat.Queries.GetMessages
{
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, QueryResult<List<ChatMessageDto>>>
    {
        private readonly IChatMessageRepository _chatRepository;

        public GetMessagesQueryHandler(IChatMessageRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<QueryResult<List<ChatMessageDto>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _chatRepository.GetByConversationAsync(request.ConversationId, cancellationToken);

            if (messages == null || !messages.Any())
                return QueryResult<List<ChatMessageDto>>.NotFoundResult("No messages found for the given conversation.");

            var result = messages.Select(m => new ChatMessageDto
            {
                Id = m.Id,
                SenderUserId = m.SenderUserId,
                Content = m.Content,
                CreatedAt = m.CreatedAt
            }).ToList();

            return QueryResult<List<ChatMessageDto>>.Success(result);
        }
    }
}
