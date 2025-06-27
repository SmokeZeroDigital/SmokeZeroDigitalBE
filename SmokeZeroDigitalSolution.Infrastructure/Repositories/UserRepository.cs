using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager;
using SmokeZeroDigitalSolution.Domain.Entites;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmokeZeroDigitalSolution.Infrastructure.DataAccessManager.Repositories
{
    public interface IUserRepository : IBaseRepository<AppUser, Guid>
    {

    }
    public class UserRepository(ApplicationDbContext applicationDbContext) : BaseRepository<AppUser, Guid>(applicationDbContext), IUserRepository
    {
    }
}
