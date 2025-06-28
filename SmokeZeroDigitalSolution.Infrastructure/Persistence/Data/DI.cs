using SmokeZeroDigitalSolution.Application.Common.IPersistence;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Data
{
    public static class DI
    {
        public static IServiceCollection RegisterDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseNpgsql(configuration.GetConnectionString("MyDB")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
