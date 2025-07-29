namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Queries
{
    public class GetPlanQueryHandler : IRequestHandler<GetPlanQuery, QueryResult<GetPlanResponseDto>>
    {
        private readonly IScriptionPlanService _planService;

        public GetPlanQueryHandler(IScriptionPlanService planService)
        {
            _planService = planService;
        }

        public async Task<QueryResult<GetPlanResponseDto>> Handle(GetPlanQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Plan == null)
                {
                    return QueryResult<GetPlanResponseDto>.Failure("Plan information is missing.");
                }
                var result = await _planService.GetPlanByPlanIdAsync(request.Plan.Id);
                return QueryResult<GetPlanResponseDto>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<GetPlanResponseDto>.Failure(ex.Message);
            }
        }
    }
}
