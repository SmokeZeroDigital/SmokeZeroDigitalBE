using SmokeZeroDigitalSolution.Application.Common.IPersistence;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.Interfaces;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Common;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Data;


namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{

    public class UserRepository(ApplicationDbContext applicationDbContext) : BaseRepository<AppUser, Guid>(applicationDbContext), IUserRepository
    {
    }
}
