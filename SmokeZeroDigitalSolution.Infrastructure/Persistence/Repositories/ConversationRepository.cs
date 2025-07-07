namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ConversationRepository(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(Conversation conversation, CancellationToken cancellationToken = default)
        {
            await _context.Conversations.AddAsync(conversation);
            await _unitOfWork.SaveAsync(cancellationToken);
        }


        public async Task<Conversation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Conversations
                .Include(c => c.User)
                .Include(c => c.Coach)
                .Include(c => c.ChatMessages)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Conversation?> GetByIdWithParticipantsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Conversations
                .Include(c => c.User)
                .Include(c => c.Coach)
                    .ThenInclude(coach => coach.User)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }


        public async Task<Conversation?> GetByUserAndCoachAsync(Guid userId, Guid coachId, CancellationToken cancellationToken = default)
        {
            return await _context.Conversations
               .Include(c => c.User)
               .Include(c => c.Coach)
                    .ThenInclude(coach => coach.User)
               .Include(c => c.ChatMessages)
               .FirstOrDefaultAsync(c => c.UserId == userId && c.CoachId == coachId && c.IsActive, cancellationToken);
        }

        public async Task UpdateAsync(Conversation conversation, CancellationToken cancellationToken = default)
        {
            _context.Conversations.Update(conversation);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

    }
}
