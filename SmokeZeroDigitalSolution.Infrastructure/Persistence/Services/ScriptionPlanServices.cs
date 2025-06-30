using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Interface;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
    public class ScriptionPlanServices(IScriptionPlanRepository scriptionPlanRepository, IUnitOfWork unitOfWork) : IScriptionPlanService
    {
        private readonly IScriptionPlanRepository _scriptionPlanRepository = scriptionPlanRepository;
        public async Task<SubscriptionPlan> CreatePlanAsync(SubscriptionPlan plan)
        {
            await _scriptionPlanRepository.AddAsync(plan);
            return plan;
        }

        public async Task<SubscriptionPlan> GetPlanByIdAsync(Guid planId)
        {
            return await _scriptionPlanRepository.FindAsync(planId);
        }

    }
}
