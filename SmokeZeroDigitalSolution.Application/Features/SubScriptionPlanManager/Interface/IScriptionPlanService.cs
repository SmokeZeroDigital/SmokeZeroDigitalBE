namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Interface
{
    public interface IScriptionPlanService
    {
        Task<CreatePlanResultDto> CreatePlanAsync(CreatePlanDTO plan);
        Task<GetPlanResponseDto> GetPlanByPlanIdAsync(Guid planId);
        Task<GetPlanResponseDto> UpdatePlanAsync(UpdatePlanDto plan);
        Task<bool> DeletePlanAsync(Guid planId);
    }
}
