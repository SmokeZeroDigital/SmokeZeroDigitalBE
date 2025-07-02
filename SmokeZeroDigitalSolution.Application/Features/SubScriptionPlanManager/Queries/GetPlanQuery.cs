using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.Plan;

namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Queries
{
    public class GetPlanQuery : IRequest<QueryResult<GetPlanResponseDto>>
    {
        public GetPlanDto Plan { get; init; } = default!;
    }
}
