using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public class ProgressEntryRepository(ApplicationDbContext context) : BaseRepository<ProgressEntry, Guid>(context),
          IProgressEntryRepository
    {
        public async Task<List<ProgressEntryDto>> GetAllAsync()
        {
            var entries = await GetAll().Select(x => new ProgressEntryDto
            {
                Id = x.Id,
                UserId = x.UserId,
                EntryDate = x.EntryDate,
                CigarettesSmokedToday = x.CigarettesSmokedToday,
                MoneySavedToday = x.MoneySavedToday,
                HealthStatusNotes = x.HealthStatusNotes,
                CravingLevel = x.CravingLevel,
                Challenges = x.Challenges,
                Successes = x.Successes,
                CreatedAt = x.CreatedAt
            }).ToListAsync();
            return entries;
        }

        public async Task<List<ProgressEntryDto>> GetByUserIdAsync(Guid userId)
        {
            var entries = await Get(x => x.UserId == userId)

                .Select(x => new ProgressEntryDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    EntryDate = x.EntryDate,
                    CigarettesSmokedToday = x.CigarettesSmokedToday,
                    MoneySavedToday = x.MoneySavedToday,
                    HealthStatusNotes = x.HealthStatusNotes,
                    CravingLevel = x.CravingLevel,
                    Challenges = x.Challenges,
                    Successes = x.Successes
                })
                .ToListAsync();
            return entries;
        }
    }
}
