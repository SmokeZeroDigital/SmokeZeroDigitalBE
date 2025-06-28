using MediatR;
using SmokeZeroDigitalSolution.Application.Common;
using SmokeZeroDigitalSolution.Application.Common.Models;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class RegisterUserCommand : IRequest<CommandResult<RegisterResultDto>>
    {
        public RegisterUserDto User { get; init; } = default!;
    }
}