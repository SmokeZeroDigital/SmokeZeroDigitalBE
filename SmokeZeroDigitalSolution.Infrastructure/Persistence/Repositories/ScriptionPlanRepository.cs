using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Interface;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public class ScriptionPlanRepository(ApplicationDbContext applicationDbContext) : BaseRepository<SubscriptionPlan, Guid>(applicationDbContext), IScriptionPlanRepository
    {
    }
}
