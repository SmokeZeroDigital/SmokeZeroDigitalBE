using SmokeZeroDigitalSolution.Application.Common.IPersistence;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Common;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Data;


namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public interface IUserRepository : IBaseRepository<AppUser, Guid>
    {

    }
    public class UserRepository(ApplicationDbContext applicationDbContext) : BaseRepository<AppUser, Guid>(applicationDbContext), IUserRepository
    {
    }
}
