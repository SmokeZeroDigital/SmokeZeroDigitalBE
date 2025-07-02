using SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDbContext _context;

        public ConversationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Conversation conversation, CancellationToken cancellationToken = default) => await
            _context.Conversations.AddAsync(conversation);

        public async Task<Conversation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Conversations
                .Include(c => c.User)
                .Include(c => c.Coach)
                .Include(c => c.ChatMessages)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Conversation?> GetByUserAndCoachAsync(Guid userId, Guid coachId, CancellationToken cancellationToken = default)
        {
            return await _context.Conversations
               .Include(c => c.User)
               .Include(c => c.Coach)
               .Include(c => c.ChatMessages)
               .FirstOrDefaultAsync(c => c.UserId == userId && c.CoachId == coachId && c.IsActive, cancellationToken);
        }
    }
}
