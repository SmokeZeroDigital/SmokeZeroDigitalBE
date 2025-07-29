using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, QueryResult<List<AppUser>>>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<QueryResult<List<AppUser>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _userService.GetAll().Where(u => u.IsDeleted != true).ToList();
            return QueryResult<List<AppUser>>.Success(users);
        }
    }
}
