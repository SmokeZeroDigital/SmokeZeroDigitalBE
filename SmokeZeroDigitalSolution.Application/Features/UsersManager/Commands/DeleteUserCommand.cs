namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class DeleteUserCommand : IRequest<CommandResult<bool>>
    {
        public Guid UserId { get; set; }
    }
}
