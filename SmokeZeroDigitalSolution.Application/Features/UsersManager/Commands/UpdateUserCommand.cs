namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class UpdateUserCommand : IRequest<CommandResult<UpdateUserResultDto>>
    {
        public UpdateUserDto User { get; set; } = default!;
    }
}
