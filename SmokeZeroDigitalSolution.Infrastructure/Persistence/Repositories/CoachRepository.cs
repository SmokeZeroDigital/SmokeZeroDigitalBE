namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public class CoachRepository : ICoachRepository
    {
        private readonly ApplicationDbContext _context;
        public CoachRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Coach coach)
        {
            _context.Coaches.Add(coach);
        }

        public async Task<List<Coach>> GetAllAsync()
        {
            return await _context.Coaches.Include(c => c.User).ToListAsync();
        }

        public async Task<Coach?> GetByIdAsync(Guid id)
        {
            return await _context.Coaches.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Coach?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Coaches.Include(c => c.User).FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task UpdateAsync(Coach coach)
        {
            _context.Coaches.Update(coach);
        }
    }
}