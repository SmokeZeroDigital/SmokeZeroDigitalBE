﻿namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public class ScriptionPlanRepository(ApplicationDbContext applicationDbContext) : BaseRepository<SubscriptionPlan, Guid>(applicationDbContext), IScriptionPlanRepository
    {
        public async Task<GetPlanResponseDto> GetPlanByIdAsync(Guid planId)
        {
            var plan = await Get(x => x.Id == planId).FirstOrDefaultAsync();
            if (plan == null)
            {
                throw new KeyNotFoundException("Subscription plan not found.");
            }
            return new GetPlanResponseDto
            {
                Name = plan.Name,
                Price = plan.Price,
                DurationInDays = plan.DurationInDays,
                Description = plan.Description,
                CreatedAt = plan.CreatedAt
            };
        }
        public async Task<CreatePlanResultDto> CreatePlanAsync(CreatePlanDTO plan)
        {
            var subscriptionPlan = new SubscriptionPlan
            {
                Name = plan.Name,
                Price = plan.Price,
                DurationInDays = plan.DurationInDays,
                Description = plan.Description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow

            };
            await AddAsync(subscriptionPlan);

            return new CreatePlanResultDto
            {
                Name = subscriptionPlan.Name,
                Description = subscriptionPlan.Description,
                Price = subscriptionPlan.Price,
                DurationInDays = subscriptionPlan.DurationInDays,
                IsActive = subscriptionPlan.IsActive,
                CreatedAt = subscriptionPlan.CreatedAt
            };
        }
        public async Task<List<GetPlanResponseDto>> GetAllSubscriptionPlans()
        {
            return await GetAll()
                .Select(plan => new GetPlanResponseDto
                {
                    Id = plan.Id,
                    Name = plan.Name,
                    Description = plan.Description,
                    Price = plan.Price,
                    DurationInDays = plan.DurationInDays,
                    IsActive = plan.IsActive,
                    CreatedAt = plan.CreatedAt
                })
                .ToListAsync();
        }
    }
}
