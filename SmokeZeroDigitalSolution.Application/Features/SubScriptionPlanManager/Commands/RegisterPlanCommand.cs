namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Commands
{
    public class RegisterPlanCommand : IRequest<CommandResult<CreatePlanResultDto>>
    {
        public CreatePlanDTO Plan { get; init; } = default!;
    }
}
