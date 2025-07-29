using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Interfaces
{
    public interface IProgressEntryRepository : IBaseRepository<ProgressEntry, Guid>
    {
        Task<List<ProgressEntryDto>> GetAllAsync();
        Task<List<ProgressEntryDto>> GetByUserIdAsync(Guid userId);
    }
}
