using SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.Commands
{
    public class CreateQuittingPlanCommand : IRequest<CommandResult<QuittingPlanDto>>
    {
        public CreateQuittingPlanDto QuittingPlan { get; init; } = default!;
    }

}
