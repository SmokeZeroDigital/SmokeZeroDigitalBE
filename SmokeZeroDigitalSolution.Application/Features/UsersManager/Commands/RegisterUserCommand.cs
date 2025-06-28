namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class RegisterUserCommand : IRequest<CommandResult<RegisterResultDto>>
    {
        public RegisterUserDto User { get; init; } = default!;
    }
}