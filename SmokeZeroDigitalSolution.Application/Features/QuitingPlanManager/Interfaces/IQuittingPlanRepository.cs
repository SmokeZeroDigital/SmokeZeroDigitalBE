using SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.Interfaces
{
    public interface IQuittingPlanRepository : IBaseRepository<QuittingPlan, Guid>
    {
        Task<QuittingPlanDto> GetByUserIdAsync(Guid userId);
        Task<List<QuittingPlanDto>> GetAllActivePlansAsync();
    }
}
