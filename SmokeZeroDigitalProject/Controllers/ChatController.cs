using SmokeZeroDigitalSolution.Application.Features.Chat.Queries.GetConversationByUserIdQuery;

namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : BaseApiController
    {
        private readonly IRequestExecutor _executor;

        public ChatController(ISender sender, IRequestExecutor executor) : base(sender)
        {
            _executor = executor;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] SendMessageRequestDto request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<SendMessageRequestDto, ChatMessageDto>(
                request,
                req => new SendMessageCommand
                {
                    Message = new SendMessageRequestDto
                    {
                        ConversationId = req.ConversationId,
                        SenderUserId = req.SenderUserId,
                        CoachId = req.CoachId,
                        Content = req.Content,
                        MessageType = req.MessageType
                    }
                },
                nameof(Send),
                cancellationToken);
        }

        [HttpGet("message/{messageId:guid}")]
        public async Task<IActionResult> GetMessageById([FromRoute] Guid messageId, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<Guid, ChatMessageDto>(
                messageId,
                id => new GetMessageByIdQuery { MessageId = id },
                nameof(GetMessageById),
                cancellationToken);
        }

        [HttpPost("conversation")]
        public async Task<IActionResult> GetOrCreateConversation([FromBody] GetOrCreateConversationQuery request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GetOrCreateConversationQuery, ConversationDto>(
                request,
                req => req,
                nameof(GetOrCreateConversation),
                cancellationToken);
        }

        [HttpGet("messages/{conversationId:guid}")]
        public async Task<IActionResult> GetMessages([FromRoute] Guid conversationId, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<Guid, List<ChatMessageDto>>(
                conversationId,
                id => new GetMessagesQuery { ConversationId = id },
                nameof(GetMessages),
                cancellationToken);
        }
        [HttpGet("conversationByUserId/{userId:guid}")]
        public async Task<IActionResult> GetConversationByUserId([FromRoute] Guid userId, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<Guid, List<ConversationDto>>(
                userId,
                req => new GetConversationByUserIdQuery { AppUserId = userId },
                nameof(GetOrCreateConversation),
                cancellationToken);
        }

    }
}
