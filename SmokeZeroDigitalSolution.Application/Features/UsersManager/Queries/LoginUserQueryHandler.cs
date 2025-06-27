using MediatR;
using Microsoft.AspNetCore.Identity;
using SmokeZeroDigitalSolution.Application.Common;
using SmokeZeroDigitalSolution.Application.Common.Models;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;
using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, QueryResult<AuthResponseDto>>
    {
        private readonly IIdentityService _identityService;

        public LoginUserQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<QueryResult<AuthResponseDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.LoginAsync(request.User);
        }
    }
}