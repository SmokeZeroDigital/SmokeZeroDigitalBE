using MediatR;
using Microsoft.AspNetCore.Identity;
using SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs;
using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, AuthResponseDto>
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public LoginUserQueryHandler(SignInManager<AppUser> signInManager,
                                      UserManager<AppUser> userManager,
                                      IJwtTokenGenerator tokenGenerator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponseDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                ?? throw new UnauthorizedAccessException("Invalid credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new UnauthorizedAccessException("Invalid credentials");

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