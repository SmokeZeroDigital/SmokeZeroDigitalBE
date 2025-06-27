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
        // Constructor đã có sẵn từ BaseApiController
        public UserController(ISender sender) : base(sender)
        {
        }

        // Đổi từ CreateCustomer thành RegisterUser
        // Bỏ [Authorize] vì đây là endpoint đăng ký, người dùng chưa được xác thực
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<ApiSuccessResult<LoginResultDto>>> RegisterUserAsync(
            [FromBody] RegisterNewUserCommand command, // Sử dụng RegisterNewUserCommand làm request
            CancellationToken cancellationToken)
        {
            // Gửi Command đến MediatR để xử lý việc đăng ký
            var response = await _sender.Send(command, cancellationToken);

            // Kiểm tra kết quả từ Command Handler
            if (response.Succeeded) // response ở đây là LoginResultDto
            {
                return Ok(new ApiSuccessResult<LoginResultDto> // return ApiSuccessResult<LoginResultDto>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "User registered successfully.", // Thông báo rõ ràng hơn
                    Content = response // Trả về LoginResultDto với UserId, UserName, Token (nếu có)
                });
            }
            else
            {
                // Xử lý trường hợp đăng ký thất bại
                // Trả về BadRequest với thông tin lỗi từ LoginResultDto.Errors
                return BadRequest(new ApiErrorResult // Giả định bạn có ApiErrorResult
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "User registration failed.",
                    Error = (Error)response.Errors
                });
            }
        }
    }
}