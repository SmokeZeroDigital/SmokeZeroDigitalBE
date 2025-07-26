using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public class SmokingRecordRepository(ApplicationDbContext db) : BaseRepository<SmokingRecord, Guid>(db), ISmokingRecordRepository
    {
        public async Task<List<SmokingRecordDto>> GetAllAsync()
        {
            return await GetAll().Select(x => new SmokingRecordDto
            {
                Id = x.Id,
                UserId = x.UserId,
                CigarettesSmoked = x.CigarettesSmoked,
                CostIncurred = x.CostIncurred,
                RecordDate = x.RecordDate,
                Notes = x.Notes
            }).ToListAsync();
        }

        public async Task<List<SmokingRecordDto>> GetByUserIdAsync(Guid userId)
        {
            return await Get(x => x.UserId == userId).Select(x => new SmokingRecordDto
            {
                Id = x.Id,
                UserId = x.UserId,
                CigarettesSmoked = x.CigarettesSmoked,
                CostIncurred = x.CostIncurred,
                RecordDate = x.RecordDate,
                Notes = x.Notes
            }).ToListAsync();
        }
    }
}
