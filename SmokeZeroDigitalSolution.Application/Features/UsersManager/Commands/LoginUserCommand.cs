using MediatR;
using SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class LoginUserCommand : IRequest<LoginResultDto>
    {
        public LoginRequest LoginRequest { get; set; }
        public LoginUserCommand(LoginRequest request)
        {
            LoginRequest = request;
        }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResultDto>
    {
        private readonly ISecurityService _securityService;
        public LoginUserCommandHandler(ISecurityService securityService)
        {
            _securityService = securityService;
        }
        public async Task<LoginResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await _securityService.LoginAsync(request.LoginRequest);
        }
    }
}