namespace SmokeZeroDigitalSolution.Application.Features.Chat.Queries.GetConversationByUserIdQuery
{
    public class GetConversationByUserIdQueryHandler : IRequestHandler<GetConversationByUserIdQuery, QueryResult<List<ConversationDto>>>
    {
        private readonly IConversationRepository _conversationRepository;
        public GetConversationByUserIdQueryHandler(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }
        public async Task<QueryResult<List<ConversationDto>>> Handle(GetConversationByUserIdQuery request, CancellationToken cancellationToken)
        {
            var conversations = await _conversationRepository.GetConversationByUserId(
                request.AppUserId,
            cancellationToken);

            if (conversations == null || !conversations.Any())
            {
                var newConversation = new Conversation
                {
                    UserId = request.AppUserId,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _conversationRepository.AddAsync(newConversation, cancellationToken);
                conversations = new List<Conversation> { newConversation };
            }

            var dtoList = conversations.Select(conversation => new ConversationDto
            {
                Id = conversation.Id,
                UserId = conversation.UserId,
                UserName = conversation.User?.UserName ?? "Unknown User",
                CoachId = conversation.CoachId,
                CoachName = conversation.Coach?.User?.UserName ?? "Unknown Coach",
                LastMessage = conversation.LastMessage,
                LastMessageSender = conversation.LastMessageSender,
                ProfilePictureUrl = conversation.User?.ProfilePictureUrl,
                CoachProfilePictureUrl = conversation.Coach?.User?.ProfilePictureUrl,
            }).ToList();

            return QueryResult<List<ConversationDto>>.Success(dtoList);
        }
    }

}


