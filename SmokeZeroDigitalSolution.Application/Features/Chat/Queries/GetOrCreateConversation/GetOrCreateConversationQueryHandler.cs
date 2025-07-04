using SmokeZeroDigitalSolution.Application.Features.Chat.DTOs;
using SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.Chat.Queries.GetOrCreateConversation
{
    public class GetOrCreateConversationQueryHandler : IRequestHandler<GetOrCreateConversationQuery, QueryResult<ConversationDto>>
    {
        private readonly IConversationRepository _conversationRepository;
        public GetOrCreateConversationQueryHandler(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }
        public async Task<QueryResult<ConversationDto>> Handle(GetOrCreateConversationQuery request, CancellationToken cancellationToken)
        {
            var conversation = await _conversationRepository.GetByUserAndCoachAsync(
                request.AppUserId,
                request.CoachId,
                cancellationToken);

            if (conversation == null)
            {
                conversation = new Conversation
                {
                    UserId = request.AppUserId,
                    CoachId = request.CoachId,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _conversationRepository.AddAsync(conversation, cancellationToken);
            }

            var dto = new ConversationDto
            {
                Id = conversation.Id,
                UserId = conversation.UserId,
                UserName = conversation.User?.UserName ?? "Unknown User",
                CoachId = conversation.CoachId,
                CoachName = conversation.Coach?.User.UserName ?? "Unknown Coach",
                LastMessage = conversation.LastMessage,
                LastMessageSender = conversation.LastMessageSender
            };

            return QueryResult<ConversationDto>.Success(dto);
        }
    }

}


