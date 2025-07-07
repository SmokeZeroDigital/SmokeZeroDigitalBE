namespace SmokeZeroDigitalSolution.Application.Features.Chat.Commands
{
    public class SendMessageCommand : IRequest<CommandResult<ChatMessageDto>>
    {
        public SendMessageRequestDto Message { get; init; } = default!;

    }
}