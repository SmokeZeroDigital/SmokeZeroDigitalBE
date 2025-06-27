using MediatR;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries
{
    public class LoginUserQuery : IRequest<AuthResponseDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}