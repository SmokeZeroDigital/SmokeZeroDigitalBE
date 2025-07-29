namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Interface
{
    public interface IScriptionPlanRepository : IBaseRepository<SubscriptionPlan, Guid>
    {
        public Task<GetPlanResponseDto> GetPlanByIdAsync(Guid planId);
        public Task<CreatePlanResultDto> CreatePlanAsync(CreatePlanDTO plan);

        public Task<List<GetPlanResponseDto>> GetAllSubscriptionPlans();

        public Task<GetPlanResponseDto> updatePlanDto(Guid planId, UpdatePlanDto updatePlanDto);
        public Task<bool> DeletePlanAsync(Guid planId);
    }
}
