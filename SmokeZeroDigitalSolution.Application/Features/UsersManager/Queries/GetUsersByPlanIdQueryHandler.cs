using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries
{
    public class GetUsersByPlanIdQueryHandler : IRequestHandler<GetUsersByPlanIdQuery, QueryResult<List<AppUser>>>
    {
        private readonly IUserService _userService;

        public GetUsersByPlanIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<QueryResult<List<AppUser>>> Handle(GetUsersByPlanIdQuery request, CancellationToken cancellationToken)
        {
            var users = _userService.Get(u => u.CurrentSubscriptionPlanId == request.PlanId && u.IsDeleted != true).ToList();
            return QueryResult<List<AppUser>>.Success(users);
        }
    }
}
