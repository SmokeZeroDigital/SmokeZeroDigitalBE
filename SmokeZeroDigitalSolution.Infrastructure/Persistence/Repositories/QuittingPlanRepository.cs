using SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.Interfaces;
using SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.DTOs;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public class QuittingPlanRepository(ApplicationDbContext context)
       : BaseRepository<QuittingPlan, Guid>(context), IQuittingPlanRepository
    {

        public async Task<List<QuittingPlanDto>> GetAllActivePlansAsync()
        {
            var plan = await Get(x => x.IsActive).Select(x => new QuittingPlanDto
            {
                Id = x.Id,
                UserId = x.UserId,
                ReasonToQuit = x.ReasonToQuit,
                CreatedAt = x.CreatedAt,
                IsActive = x.IsActive
            })
                .ToListAsync();
            return plan;
        }

        public async Task<QuittingPlanDto> GetByUserIdAsync(Guid userId)
        {
            var plan = await Get(x => x.UserId == userId).Select(plan => new QuittingPlanDto
            {
                Id = plan.Id,
                UserId = plan.UserId,
                ReasonToQuit = plan.ReasonToQuit,
                StartDate = plan.StartDate,
                ExpectedEndDate = plan.ExpectedEndDate,
                InitialCigarettesPerDay = plan.InitialCigarettesPerDay,
                InitialCostPerCigarette = plan.InitialCostPerCigarette,
                CreatedAt = plan.CreatedAt,
                IsActive = plan.IsActive,
                Stages = plan.Stages,
                CustomNotes = plan.CustomNotes
            }).FirstOrDefaultAsync();

            return plan ?? throw new KeyNotFoundException("Quitting plan not found for the specified user.");
        }

    }
}
