namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
    public class ScriptionPlanServices(IScriptionPlanRepository scriptionPlanRepository, IUnitOfWork unitOfWork) : IScriptionPlanService
    {
        private readonly IScriptionPlanRepository _scriptionPlanRepository = scriptionPlanRepository;
        public async Task<CreatePlanResultDto> CreatePlanAsync(CreatePlanDTO plan)
        {
             return await _scriptionPlanRepository.CreatePlanAsync(plan);
        }

        public Task<bool> DeletePlanAsync(Guid planId)
        {
            var task = _scriptionPlanRepository.DeletePlanAsync(planId);
            if (task.IsCompletedSuccessfully)
            {
                return Task.FromResult(true);
            }
            else
            {
                throw new Exception("Failed to delete the plan.");
            }
        }

        public async Task<GetPlanResponseDto> GetPlanByPlanIdAsync(Guid planId)
        {
            return await _scriptionPlanRepository.GetPlanByIdAsync(planId);
        }

        public async Task<GetPlanResponseDto> UpdatePlanAsync(UpdatePlanDto plan)
        {
            return await _scriptionPlanRepository.updatePlanDto(plan.Id, plan);
        }
    }
}
