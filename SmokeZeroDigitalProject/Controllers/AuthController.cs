using SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.Interfaces;
using SmokeZeroDigitalSolution.Contracts.Auth;

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

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GoogleLoginRequest, AuthResponseDto>(
                request,
                req => new GoogleLoginQuery
                {
                    User = new GoogleLoginDTO
                    {
                        Token = req.Token
                    }
                },
                nameof(GoogleLogin),
                cancellationToken);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<ConfirmEmailRequest, ConfirmEmailResultDto>(
                request,
                req => new ConfirmEmailCommand
                {
                    ConfirmEmailDto = new ConfirmEmailDto
                    {
                        UserId = req.UserId,
                        Token = req.Token
                    }
                },
                nameof(ConfirmEmail),
                cancellationToken);
        }

        [HttpPost("confirm-email-by-email")]
        public async Task<IActionResult> ConfirmEmailByEmail([FromBody] ConfirmEmailByEmailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var authService = HttpContext.RequestServices.GetRequiredService<IAuthService>();
                var result = await authService.ConfirmEmailAsync(request.Email, request.Token);
                
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ConfirmEmailResultDto 
                { 
                    Success = false, 
                    Message = ex.Message 
                });
            }
        }
    }
}
