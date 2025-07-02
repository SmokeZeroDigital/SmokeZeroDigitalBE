namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
    public class ScriptionPlanServices(IScriptionPlanRepository scriptionPlanRepository, IUnitOfWork unitOfWork) : IScriptionPlanService
    {
        private readonly IScriptionPlanRepository _scriptionPlanRepository = scriptionPlanRepository;
        public async Task<CreatePlanResultDto> CreatePlanAsync(CreatePlanDTO plan)
        {
             return await _scriptionPlanRepository.CreatePlanAsync(plan);
        }

        public async Task<GetPlanResponseDto> GetPlanByPlanIdAsync(Guid planId)
        {
            return await _scriptionPlanRepository.GetPlanByIdAsync(planId);
        }

    }
}
