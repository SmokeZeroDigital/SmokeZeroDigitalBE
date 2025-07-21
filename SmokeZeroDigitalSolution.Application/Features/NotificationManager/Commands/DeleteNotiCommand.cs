namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.Commands
{
    public class DeleteNotiCommand : IRequest<CommandResult<bool>>
    {
        public Guid Id { get; init; } = default!;
    }
    
}
