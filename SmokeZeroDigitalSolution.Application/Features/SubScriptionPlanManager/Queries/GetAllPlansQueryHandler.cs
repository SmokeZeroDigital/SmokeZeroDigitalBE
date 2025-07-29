using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Queries
{
    public class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, QueryResult<List<GetPlanResponseDto>>>
    {
        private readonly IScriptionPlanRepository _repository;

        public GetAllPlansQueryHandler(IScriptionPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<QueryResult<List<GetPlanResponseDto>>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            var plans = await _repository.GetAllSubscriptionPlans();
            var result = plans.Select(plan => new GetPlanResponseDto
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationInDays = plan.DurationInDays,
                IsActive = plan.IsActive,
                CreatedAt = plan.CreatedAt
            }).ToList();

            return QueryResult<List<GetPlanResponseDto>>.Success(result);
        }
    }

}
