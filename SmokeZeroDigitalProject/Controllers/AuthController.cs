using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmokeZeroDigitalProject.Common.Base;
using SmokeZeroDigitalProject.Common.Models;
using SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries;

namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        public AuthController(ISender sender) : base(sender) { }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            // Gửi command tới handler
            var command = new RegisterUserCommand
            {
                Email = request.Email,
                Password = request.Password,
                FullName = request.FullName,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth
            };

            var response = await _sender.Send(command, cancellationToken);

            if (!string.IsNullOrEmpty(response.Token))
            {
                return Ok(new ApiSuccessResult<AuthResponseDto>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Registration successful.",
                    Content = response
                });
            }
            else
            {
                return BadRequest(new ApiErrorResult
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Registration failed.",
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
            // Gửi query tới handler
            var query = new LoginUserQuery
            {
                Email = request.UserName,
                Password = request.Password
            };

            var response = await _sender.Send(query, cancellationToken);

            if (!string.IsNullOrEmpty(response.Token))
            {
                return Ok(new ApiSuccessResult<AuthResponseDto>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Login successful.",
                    Content = response
                });
            }
            else
            {
                return Unauthorized(new ApiErrorResult
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Invalid email or password.",
                    Error = response.Error != null && response.Errors.Count > 0
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