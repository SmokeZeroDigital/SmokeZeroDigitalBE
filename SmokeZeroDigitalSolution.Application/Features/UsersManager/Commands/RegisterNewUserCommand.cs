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
        public RegisterRequest RegisterRequest { get; set; }
        public RegisterNewUserCommand(RegisterRequest request)
        {
            RegisterRequest = request;
        }
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
            return await _securityService.RegisterAsync(request.RegisterRequest);
        }
    }
}