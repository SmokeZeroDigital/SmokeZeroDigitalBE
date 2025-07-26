using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Interfaces
{
    public interface ISmokingRecordRepository : IBaseRepository<SmokingRecord, Guid>
    {
        Task<List<SmokingRecordDto>> GetAllAsync();
        Task<List<SmokingRecordDto>> GetByUserIdAsync(Guid userId);

    }
}
