using SmokeZeroDigitalSolution.Application.Common.IPersistence;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Data;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _applicationDbContext;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }
    }
}
