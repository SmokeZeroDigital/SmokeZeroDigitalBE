using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmokeZeroDigitalProject.Common.Base;
using SmokeZeroDigitalProject.Common.Models;
using SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands;

namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        public UserController(ISender sender) : base(sender) { }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(new RegisterNewUserCommand(request), cancellationToken);
            if (!string.IsNullOrEmpty(response.Token))
            {
                return Ok(new ApiSuccessResult<LoginResultDto>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Đăng ký thành công.",
                    Content = response
                });
            }
            else
            {
                return BadRequest(new ApiErrorResult
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Đăng ký thất bại.",
                    Error = response.Errors != null && response.Errors.Count > 0
                        ? new Error(
                            innerException: string.Join("; ", response.Errors),
                            source: nameof(Register),
                            stackTrace: null,
                            exceptionType: "RegisterException")
                        : null
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(new LoginUserCommand(request), cancellationToken);
            if (!string.IsNullOrEmpty(response.Token))
            {
                return Ok(new ApiSuccessResult<LoginResultDto>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Đăng nhập thành công.",
                    Content = response
                });
            }
            else
            {
                return Unauthorized(new ApiErrorResult
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Sai tài khoản hoặc mật khẩu.",
                    Error = response.Errors != null && response.Errors.Count > 0
                        ? new Error(
                            innerException: string.Join("; ", response.Errors),
                            source: nameof(Login),
                            stackTrace: null,
                            exceptionType: "LoginException")
                        : null
                });
            }
        }
    }
}