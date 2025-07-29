using SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.Interfaces;
using SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.DTOs;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
    public class QuittingPlanService(IQuittingPlanRepository quittingPlanRepository, IUnitOfWork unitOfWork) : IQuittingPlanService
    {
        public async Task<QuittingPlanDto> CreateAsync(CreateQuittingPlanDto dto)
        {
            var entity = new QuittingPlan
            {
                UserId = dto.UserId,
                ReasonToQuit = dto.ReasonToQuit,
                StartDate = dto.StartDate,
                ExpectedEndDate = dto.ExpectedEndDate,
                Stages = dto.Stages,
                CustomNotes = dto.CustomNotes,
                InitialCigarettesPerDay = dto.InitialCigarettesPerDay,
                InitialCostPerCigarette = dto.InitialCostPerCigarette,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await quittingPlanRepository.AddAsync(entity);

            return new QuittingPlanDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ReasonToQuit = entity.ReasonToQuit,
                StartDate = entity.StartDate,
                ExpectedEndDate = entity.ExpectedEndDate,
                CreatedAt = entity.CreatedAt,
                IsActive = entity.IsActive,
                InitialCigarettesPerDay = dto.InitialCigarettesPerDay,
                InitialCostPerCigarette = dto.InitialCostPerCigarette,

            };
        }

        public async Task<QuittingPlanDto> GetByUserIdAsync(Guid userId)
        {
            return await quittingPlanRepository.GetByUserIdAsync(userId);
        }

        public async Task<List<QuittingPlanDto>> GetAllAsync()
        {
            return await quittingPlanRepository.GetAllActivePlansAsync();
        }
    }
}
