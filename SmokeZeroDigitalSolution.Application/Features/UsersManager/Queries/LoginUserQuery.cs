using MediatR;
using SmokeZeroDigitalSolution.Application.Common.Models;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries
{
    public class LoginUserQuery : IRequest<QueryResult<AuthResponseDto>>
    {
        public LoginUserDto User { get; init; } = default!;
    }
}