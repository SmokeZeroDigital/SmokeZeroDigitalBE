using MediatR;
using SmokeZeroDigitalSolution.Application.Common.Models;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.Interfaces;


namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, CommandResult<RegisterResultDto>>
    {
        private readonly IAuthService _jwtService;

        public RegisterUserCommandHandler(IAuthService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task<CommandResult<RegisterResultDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _jwtService.RegisterAsync(request.User, cancellationToken);
                return CommandResult<RegisterResultDto>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<RegisterResultDto>.Failure(ex.Message);
            }
        }
    }
}
