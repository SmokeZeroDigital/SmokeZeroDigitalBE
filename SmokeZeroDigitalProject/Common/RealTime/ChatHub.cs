namespace SmokeZeroDigitalProject.Common.Realtime;

public class ChatHub : Hub
{
    public async Task JoinConversation(string conversationId)
    {
        Console.WriteLine($"✅ {Context.ConnectionId} joined conversation {conversationId}");

        await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
    }

    public async Task LeaveConversation(string conversationId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId);
    }
}
