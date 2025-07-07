namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, QueryResult<AppUser>>
    {
        private readonly IUserService _userService;

        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<QueryResult<AppUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindAsync(request.UserId);
            if (user == null || user.IsDeleted == true)
                return QueryResult<AppUser>.NotFoundResult("User not found");
            return QueryResult<AppUser>.Success(user);
        }
    }
}