using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class DeleteUserCommand : IRequest<CommandResult<bool>>
    {
        public Guid UserId { get; set; }
    }
}
