using MediatR;
using SmokeZeroDigitalSolution.Application.Common.Models;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;


namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, CommandResult<AuthResponseDto>>
    {
        private readonly IIdentityService _identityService;

        public RegisterUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<CommandResult<AuthResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.RegisterAsync(request.User);
        }
    }
}
