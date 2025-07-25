using SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.Interfaces;
using SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.Queries
{
    public class GetQuittingPlanByUserIdQueryHandler : IRequestHandler<GetQuittingPlanByUserIdQuery, QueryResult<QuittingPlanDto>>
    {
        private readonly IQuittingPlanService _service;
        public GetQuittingPlanByUserIdQueryHandler(IQuittingPlanService service)
        {
            _service = service;
        }
        public async Task<QueryResult<QuittingPlanDto>> Handle(GetQuittingPlanByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetByUserIdAsync(request.UserId);
                return QueryResult<QuittingPlanDto>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<QuittingPlanDto>.Failure(ex.Message);
            }
        }
    }
}
