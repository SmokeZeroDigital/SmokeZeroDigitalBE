using SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Payment
{
    public static class DI
    {
        public static IServiceCollection RegisterPaymentPlan(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IScriptionPlanRepository, ScriptionPlanRepository>();
            services.AddScoped<IScriptionPlanService, ScriptionPlanServices>();
            return services;
        }
    }
}
