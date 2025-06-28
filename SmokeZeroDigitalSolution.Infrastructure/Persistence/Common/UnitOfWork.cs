namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _applicationDbContext;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
