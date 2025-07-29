namespace SmokeZeroDigitalSolution.Application.Common.Interfaces;

public interface IChatNotifier
{
    Task NotifyNewMessageAsync(ChatMessageDto message, CancellationToken cancellationToken);
}
