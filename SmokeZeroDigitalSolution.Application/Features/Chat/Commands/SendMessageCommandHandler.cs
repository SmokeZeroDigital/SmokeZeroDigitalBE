using SmokeZeroDigitalSolution.Application.Common.Interfaces;
using SmokeZeroDigitalSolution.Application.Features.Chat.Commands;
using SmokeZeroDigitalSolution.Application.Features.Chat.DTOs;
using SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.Chat.Handlers;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, CommandResult<ChatMessageDto>>
{
    private readonly IChatMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IChatNotifier _notifier;

    public SendMessageCommandHandler(
        IChatMessageRepository messageRepository,
        IUnitOfWork unitOfWork,
        IChatNotifier notifier)
    {
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
        _notifier = notifier;
    }

    public async Task<CommandResult<ChatMessageDto>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Message;

        var chatMessage = new ChatMessage
        {
            ConversationId = dto.ConversationId,
            SenderUserId = dto.SenderUserId,
            CoachId = dto.CoachId,
            Content = dto.Content,
            MessageType = dto.MessageType,
            IsRead = false,
            Timestamp = DateTime.UtcNow
        };

        _messageRepository.Add(chatMessage);
        await _unitOfWork.SaveAsync(cancellationToken);

        var response = new ChatMessageDto
        {
            Id = chatMessage.Id,
            ConversationId = chatMessage.ConversationId,
            SenderUserId = chatMessage.SenderUserId,
            CoachId = chatMessage.CoachId,
            Content = chatMessage.Content,
            Timestamp = chatMessage.Timestamp,
            MessageType = chatMessage.MessageType,
            IsRead = chatMessage.IsRead
        };

        await _notifier.NotifyNewMessageAsync(response, cancellationToken);

        return CommandResult<ChatMessageDto>.Success(response);
    }
}
