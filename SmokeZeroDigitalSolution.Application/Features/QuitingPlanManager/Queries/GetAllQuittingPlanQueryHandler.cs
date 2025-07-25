using SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.Interfaces;
using SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.Queries
{
    public class GetAllQuittingPlanQueryHandler : IRequestHandler<GetAllQuittingPlanQuery, QueryResult<List<QuittingPlanDto>>>
    {
        private readonly IQuittingPlanRepository _service;
        public GetAllQuittingPlanQueryHandler(IQuittingPlanRepository service)
        {
            _service = service;
        }
        public async Task<QueryResult<List<QuittingPlanDto>>> Handle(GetAllQuittingPlanQuery request, CancellationToken cancellationToken)

        {
            try
            {
                var result = await _service.GetAllActivePlansAsync();
                return QueryResult<List<QuittingPlanDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<List<QuittingPlanDto>>.Failure(ex.Message);
            }
        }
    }
}
