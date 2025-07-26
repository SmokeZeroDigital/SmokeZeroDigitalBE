using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Interfaces
{
    public interface IProgressEntryService
    {
        Task<ProgressEntryDto> CreateAsync(CreateProgressEntryDto dto);
        Task<List<ProgressEntryDto>> GetAllAsync();
        Task<List<ProgressEntryDto>> GetByUserIdAsync(Guid userId);
    }
}
