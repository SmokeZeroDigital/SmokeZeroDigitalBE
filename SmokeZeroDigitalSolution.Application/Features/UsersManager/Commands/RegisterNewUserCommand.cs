using MediatR;
using SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class RegisterNewUserCommand : IRequest<LoginResultDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public int Gender { get; set; } // 0: Unknown, 1: Male, 2: Female
    }

    // Handler: Xử lý logic khi nhận được RegisterNewUserCommand
    public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, LoginResultDto>
    {
        private readonly ISecurityService _securityService;

        public RegisterNewUserCommandHandler(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public async Task<LoginResultDto> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            // 1. Tạo đối tượng RegisterRequest từ Command
            var registerRequest = new RegisterRequest
            {
                Email = request.Email,
                Password = request.Password,
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender
            };

            // 2. Gọi SecurityService để thực hiện việc đăng ký người dùng
            var result = await _securityService.RegisterUserAsync(registerRequest);

            // 3. Trả về kết quả từ SecurityService
            return result;
        }
    }
}