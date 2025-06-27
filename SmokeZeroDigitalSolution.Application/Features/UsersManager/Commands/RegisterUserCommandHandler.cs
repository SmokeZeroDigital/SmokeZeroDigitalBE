using MediatR;
using Microsoft.AspNetCore.Identity;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs;
using SmokeZeroDigitalSolution.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponseDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public RegisterUserCommandHandler(UserManager<AppUser> userManager, IJwtTokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                UserName = request.Email,
                FullName = request.FullName,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                RegistrationDate = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    Errors = result.Errors.Select(e => e.Description).ToList(),
                    Error = "Identity creation failed"
                };
            }
            var token = _tokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email!,
                Token = token
            };
        }
    }

}
