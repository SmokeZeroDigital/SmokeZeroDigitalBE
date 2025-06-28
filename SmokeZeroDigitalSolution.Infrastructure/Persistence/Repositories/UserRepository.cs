namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{

    public class UserRepository(ApplicationDbContext applicationDbContext) : BaseRepository<AppUser, Guid>(applicationDbContext), IUserRepository
    {
    }
}
