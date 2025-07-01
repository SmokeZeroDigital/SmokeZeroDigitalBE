using SmokeZeroDigitalSolution.Application.Features.Chat.Commands;
using SmokeZeroDigitalSolution.Application.Features.Chat.DTOs;

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
    }
}
