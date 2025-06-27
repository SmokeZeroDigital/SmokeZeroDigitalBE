using MediatR;
using SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class RegisterUserCommand : IRequest<AuthResponseDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public int Gender { get; set; }
    }
}