namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands;

public class ConfirmEmailCommand : IRequest<CommandResult<ConfirmEmailResultDto>>
{
    public ConfirmEmailDto ConfirmEmailDto { get; set; }
}
