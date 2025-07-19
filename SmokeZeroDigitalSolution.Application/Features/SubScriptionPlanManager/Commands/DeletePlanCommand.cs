namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Commands
{
    public class DeletePlanCommand : IRequest<CommandResult<bool>>
    {
        public Guid PlanId { get; set; }
    }
}
