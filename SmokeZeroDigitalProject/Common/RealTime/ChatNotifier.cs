namespace SmokeZeroDigitalProject.Common.Realtime;

public class ChatNotifier : IChatNotifier
{
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatNotifier(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyNewMessageAsync(ChatMessageDto message, CancellationToken cancellationToken)
    {
        await _hubContext.Clients
            .Group(message.ConversationId.ToString())
            .SendAsync("ReceiveMessage", message, cancellationToken);
    }
}
