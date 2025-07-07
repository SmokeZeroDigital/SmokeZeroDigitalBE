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
    }
}