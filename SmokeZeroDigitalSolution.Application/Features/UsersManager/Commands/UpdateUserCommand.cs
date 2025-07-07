using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class UpdateUserCommand : IRequest<CommandResult<UpdateUserResultDto>>
    {
        public UpdateUserDto User { get; set; } = default!;
    }
}
