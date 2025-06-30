using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Interface
{
    public interface IScriptionPlanService
    {
        Task<SubscriptionPlan> CreatePlanAsync(SubscriptionPlan plan);
        Task<SubscriptionPlan> GetPlanByIdAsync(Guid planId);
    }
}
