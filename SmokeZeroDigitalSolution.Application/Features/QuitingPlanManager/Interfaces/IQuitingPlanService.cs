using SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.Interfaces
{
    public interface IQuittingPlanService
    {
        Task<QuittingPlanDto> CreateAsync(CreateQuittingPlanDto dto);
        Task<QuittingPlanDto> GetByUserIdAsync(Guid userId);
        Task<List<QuittingPlanDto>> GetAllAsync();
    }

}
