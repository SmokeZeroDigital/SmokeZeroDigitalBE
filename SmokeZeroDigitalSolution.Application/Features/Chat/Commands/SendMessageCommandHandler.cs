using SmokeZeroDigitalSolution.Application.Features.Chat.Commands;
using SmokeZeroDigitalSolution.Application.Features.Chat.DTOs;
using SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, CommandResult<ChatMessageDto>>
{
    private readonly IChatService _chatService;

    public SendMessageCommandHandler(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task<CommandResult<ChatMessageDto>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _chatService.SendMessageAsync(request.Message, cancellationToken);
            return CommandResult<ChatMessageDto>.Success(result);
        }
        catch (Exception ex)
        {
            return CommandResult<ChatMessageDto>.Failure(ex.Message);
        }
    }
}
