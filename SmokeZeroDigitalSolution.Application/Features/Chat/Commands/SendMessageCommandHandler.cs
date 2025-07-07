﻿namespace SmokeZeroDigitalSolution.Application.Features.Chat.Handlers;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, CommandResult<ChatMessageDto>>
{
    private readonly IChatMessageRepository _messageRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IChatNotifier _notifier;

    public SendMessageCommandHandler(
        IChatMessageRepository messageRepository,
        IUnitOfWork unitOfWork,
        IChatNotifier notifier,
        IConversationRepository conversationRepository)
    {
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
        _notifier = notifier;
        _conversationRepository = conversationRepository;
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
            Timestamp = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,

        };

        _messageRepository.Add(chatMessage);

        var conversation = await _conversationRepository.GetByIdAsync(dto.ConversationId, cancellationToken);
        if (conversation != null)
        {
            conversation.LastMessage = dto.Content;

            conversation.LastMessageSender = dto.CoachId.HasValue
                ? conversation.Coach?.User?.UserName ?? "Unknown Coach"
                : conversation.User?.UserName ?? "Unknown User";

            await _conversationRepository.UpdateAsync(conversation, cancellationToken);
        }


        var response = new ChatMessageDto
        {
            Id = chatMessage.Id,
            ConversationId = chatMessage.ConversationId,
            SenderUserId = chatMessage.SenderUserId,
            CoachId = chatMessage.CoachId,
            Content = chatMessage.Content,
            Timestamp = chatMessage.Timestamp,
            MessageType = chatMessage.MessageType,
            IsRead = chatMessage.IsRead,
            CreatedAt = DateTime.UtcNow,

        };

        await _notifier.NotifyNewMessageAsync(response, cancellationToken);

        return CommandResult<ChatMessageDto>.Success(response);
    }
}
