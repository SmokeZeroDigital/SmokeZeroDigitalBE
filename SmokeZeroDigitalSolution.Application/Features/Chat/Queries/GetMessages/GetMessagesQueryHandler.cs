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
                ConversationId = m.ConversationId,
                SenderUserId = m.SenderUserId,
                CoachId = m.CoachId,
                Content = m.Content,
                MessageType = m.MessageType,
                Timestamp = m.Timestamp,
                IsRead = m.IsRead,
                CreatedAt = m.Timestamp,

                User = m.User != null ? new AppUserShortDto
                {
                    Id = m.User.Id,
                    FullName = m.User.FullName,
                    ProfilePictureUrl = m.User.ProfilePictureUrl
                } : null,

                Coach = m.Coach != null ? new CoachShortDto
                {
                    Id = m.Coach.Id,
                    FullName = $"{m.Coach.User.FullName}",
                    ProfilePictureUrl = m.Coach.User.ProfilePictureUrl
                } : null

            }).ToList();

            return QueryResult<List<ChatMessageDto>>.Success(result);
        }
    }
}
