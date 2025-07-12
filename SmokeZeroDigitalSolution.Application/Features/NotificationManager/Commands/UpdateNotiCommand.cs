using SmokeZeroDigitalSolution.Application.Features.NotificationManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.Commands
{
    public class UpdateNotiCommand : IRequest<CommandResult<Notification>>
    {
        public CreateNotiDto Dto { get; init; } = default!;
    }
}
