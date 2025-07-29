using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Interfaces
{
    public interface ISmokingRecordService
    {
        Task<SmokingRecordDto> CreateAsync(CreateSmokingRecordDto dto);
        Task<List<SmokingRecordDto>> GetAllAsync();
        Task<List<SmokingRecordDto>> GetByUserIdAsync(Guid userId);
    }
}
