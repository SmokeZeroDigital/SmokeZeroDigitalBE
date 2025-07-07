﻿namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Interface
{
    public interface IScriptionPlanRepository : IBaseRepository<SubscriptionPlan, Guid>
    {
        public Task<GetPlanResponseDto> GetPlanByIdAsync(Guid planId);
        public Task<CreatePlanResultDto> CreatePlanAsync(CreatePlanDTO plan);
    }
}
