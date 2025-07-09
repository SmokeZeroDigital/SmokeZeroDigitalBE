namespace SmokeZeroDigitalSolution.Application.Features.CoachManager.Interfaces
{
    public interface ICoachRepository
    {
        Task<Coach?> GetByIdAsync(Guid id);
        Task<Coach?> GetByUserIdAsync(Guid userId);
        Task<List<Coach>> GetAllAsync();
        Task AddAsync(Coach coach);
        Task UpdateAsync(Coach coach);
    }
}
