using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
    public class ProgresEntryService(IProgressEntryRepository repo, IUnitOfWork unitOfWork) : IProgressEntryService
    {
        public async Task<ProgressEntryDto> CreateAsync(CreateProgressEntryDto dto)
        {
            var entity = new ProgressEntry
            {
                UserId = dto.UserId,
                EntryDate = dto.EntryDate,
                CigarettesSmokedToday = dto.CigarettesSmokedToday,
                MoneySavedToday = dto.MoneySavedToday,
                HealthStatusNotes = dto.HealthStatusNotes,
                CravingLevel = dto.CravingLevel,
                Challenges = dto.Challenges,
                Successes = dto.Successes,
                CreatedAt = DateTime.UtcNow
            };

            await repo.AddAsync(entity);
            await unitOfWork.SaveAsync();

            return new ProgressEntryDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                EntryDate = entity.EntryDate,
                CigarettesSmokedToday = entity.CigarettesSmokedToday,
                MoneySavedToday = entity.MoneySavedToday,
                HealthStatusNotes = entity.HealthStatusNotes,
                CravingLevel = entity.CravingLevel,
                Challenges = entity.Challenges,
                Successes = entity.Successes,
                CreatedAt = entity.CreatedAt
            };
        }

        public async Task<List<ProgressEntryDto>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<List<ProgressEntryDto>> GetByUserIdAsync(Guid userId)
        {
            return await repo.GetByUserIdAsync(userId);
        }
    }
}
