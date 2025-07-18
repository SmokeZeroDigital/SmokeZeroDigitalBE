using SmokeZeroDigitalSolution.Application.Features.Chat.Queries;
using SmokeZeroDigitalSolution.Contracts.Conversation;
using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IRequestExecutor _executor;

        public UserController(ISender sender, IRequestExecutor executor) : base(sender)
        {
            _executor = executor;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<DeleteUserRequest, bool>(
                request,
                req => new DeleteUserCommand { UserId = req.UserId },
                nameof(Delete),
                cancellationToken
            );
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<UpdateUserRequest, UpdateUserResultDto>(
                request,
                req => new UpdateUserCommand
                {
                    User = new UpdateUserDto
                    {
                        UserId = req.UserId,
                        PlanId = req.PlanId,
                        Email = req.Email,
                        DateOfBirth = req.DateOfBirth,
                        FullName = req.FullName,
                        EmailConfirmed = req.EmailConfirmed,
                        IsDeleted = req.IsDeleted
                    }
                },
                nameof(Update),
                cancellationToken
            );
        }
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetById(Guid userId, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GetUserByIdQuery, AppUser>(
                new GetUserByIdQuery { UserId = userId },
                _ => new GetUserByIdQuery { UserId = userId },
                nameof(GetById),
                cancellationToken
            );
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GetAllUsersQuery, List<AppUser>>(
                new GetAllUsersQuery(),
                _ => new GetAllUsersQuery(),
                nameof(GetAll),
                cancellationToken
            );
        }

        [HttpGet("by-plan/{planId:guid}")]
        public async Task<IActionResult> GetByPlanId(Guid planId, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GetUsersByPlanIdQuery, List<AppUser>>(
                new GetUsersByPlanIdQuery { PlanId = planId },
                _ => new GetUsersByPlanIdQuery { PlanId = planId },
                nameof(GetByPlanId),
                cancellationToken
            );
        }

        [HttpGet("list-coaches")]
        public async Task<IActionResult> GetCoachByUser([FromBody] GetListCoachesOrUser request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GetListCoachesOrUser, List<UserInfoDto>>(
                request,
                req => new GetCoachByUserIdQuery
                {
                    UserId = req.Id
                },

                nameof(GetCoachByUser),
                cancellationToken);
        }
    }
}