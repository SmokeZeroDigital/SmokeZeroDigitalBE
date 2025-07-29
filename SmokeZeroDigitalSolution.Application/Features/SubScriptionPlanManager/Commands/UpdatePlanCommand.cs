
namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Commands
{
    public class UpdatePlanCommand : IRequest<CommandResult<GetPlanResponseDto>>
    {
        public UpdatePlanDto Plan { get; set; }
    }
}
