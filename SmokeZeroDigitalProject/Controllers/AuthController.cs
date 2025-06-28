namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IRequestExecutor _executor;

        public AuthController(ISender sender, IRequestExecutor executor) : base(sender)
        {
            _executor = executor;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<RegisterRequest, RegisterResultDto>(
                request,
                req => new RegisterUserCommand
                {
                    User = new RegisterUserDto
                    {
                        Email = req.Email,
                        Username = req.Username,
                        Password = req.Password,
                        ConfirmPassword = req.ConfirmPassword,
                        FullName = req.FullName,
                        DateOfBirth = req.DateOfBirth,
                        Gender = req.Gender
                    }
                },
                nameof(Register),
                cancellationToken);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<LoginRequest, AuthResponseDto>(
                request,
                req => new LoginUserQuery
                {
                    User = new LoginUserDto
                    {
                        Username = req.UserName,
                        Password = req.Password
                    }
                },
                nameof(Login),
                cancellationToken);
        }
    }
}