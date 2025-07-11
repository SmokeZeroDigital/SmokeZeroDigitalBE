namespace SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Interfaces
{
    public interface IProgressEntryRepository : IBaseRepository<ProgressEntry, Guid>
    {
        Task<List<ProgressEntry>> GetAllAsync();
        Task<List<ProgressEntry>> GetByUserIdAsync(Guid userId);
    }
}
