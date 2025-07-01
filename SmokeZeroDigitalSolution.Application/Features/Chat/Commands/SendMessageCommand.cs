using SmokeZeroDigitalSolution.Application.Features.Chat.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.Chat.Commands
{
    public class SendMessageCommand : IRequest<CommandResult<ChatMessageDto>>
    {
        public SendMessageDto Message { get; init; } = default!;

    }
}