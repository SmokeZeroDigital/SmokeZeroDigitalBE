using SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.Queries
{
    public class GetQuittingPlanByUserIdQuery : IRequest<QueryResult<QuittingPlanDto>>
    {
        public Guid UserId { get; set; }
    }
}
