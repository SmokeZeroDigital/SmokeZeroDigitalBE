namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, CommandResult<UpdateUserResultDto>>
    {
        private readonly IAuthService _authService;

        public UpdateUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<CommandResult<UpdateUserResultDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authService.UpdateUserAsync(request.User, cancellationToken);
                return CommandResult<UpdateUserResultDto>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<UpdateUserResultDto>.Failure(ex.Message);
            }
        }
    }
}
