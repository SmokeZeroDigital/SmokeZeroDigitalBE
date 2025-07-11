using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
    public class SmokingRecordService(ISmokingRecordRepository repo, UnitOfWork unitOfWork)
        : ISmokingRecordService
    {
        public async Task<SmokingRecordDto> CreateAsync(CreateSmokingRecordDto dto)
        {
            var entity = new SmokingRecord
            {
                UserId = dto.UserId,
                CigarettesSmoked = dto.CigarettesSmoked,
                CostIncurred = dto.CostIncurred,
                RecordDate = dto.RecordDate,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow
            };
            await repo.AddAsync(entity);
            await unitOfWork.SaveAsync();
            return new SmokingRecordDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                CigarettesSmoked = entity.CigarettesSmoked,
                CostIncurred = entity.CostIncurred,
                RecordDate = entity.RecordDate,
                Notes = entity.Notes
            };
        }

        public async Task<List<SmokingRecordDto>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<List<SmokingRecordDto>> GetByUserIdAsync(Guid userId)
        {
            return await repo.GetByUserIdAsync(userId);
        }
    }
}
