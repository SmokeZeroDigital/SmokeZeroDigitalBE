namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Commands
{
    public class VNPayCallbackCommand : IRequest<CommandResult<VNPayCallbackResultDto>>
    {
        public IQueryCollection Query { get; set; }
    }
}
