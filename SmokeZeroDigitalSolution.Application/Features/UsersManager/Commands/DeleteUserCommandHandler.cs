using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, CommandResult<bool>>
    {
        private readonly IAuthService _authService;

        public DeleteUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<CommandResult<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.DeleteAsync(request.UserId, cancellationToken);
            if (!result)
                return CommandResult<bool>.Failure("User not found or already deleted.");

            return CommandResult<bool>.Success(true);
        }
    }
}
