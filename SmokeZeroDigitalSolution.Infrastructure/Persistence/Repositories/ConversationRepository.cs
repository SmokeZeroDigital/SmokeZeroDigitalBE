using SmokeZeroDigitalSolution.Application.Features.Chat.DTOs;

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


        public async Task<List<UserInfoDto>> GetUsersByCoachIdAsync(Guid coachId)
        {
            var users = await _context.Conversations
            .Where(c => c.CoachId == coachId)
            .Select(c => c.User)
            .Distinct()
            .ToListAsync();
            return users.Select(u => new UserInfoDto
            {
                Id = u.Id,
                FullName = u.FullName
            }).ToList();
        }

        public async Task<List<UserInfoDto>> GetCoachsByUserIdAsync(Guid userId)
        {
            var coaches = await _context.Conversations
            .Where(c => c.UserId == userId)
            .Select(c => c.Coach)
            .Distinct()
            .ToListAsync();
            return coaches.Select(u => new UserInfoDto
            {
                Id = u.Id,
                FullName = u.User.FullName
            }).ToList();
        }


        public async Task<List<Conversation>> GetConversationByUserId(Guid appUserId, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Coach)
                .FirstOrDefaultAsync(u => u.Id == appUserId, cancellationToken);

            if (user == null)
                return new();

            var query = _context.Conversations
                .Include(c => c.User)
                .Include(c => c.Coach)
                    .ThenInclude(coach => coach.User)
                .Include(c => c.ChatMessages)
                .Where(c => c.IsActive);

            if (user.Coach != null)
            {
                query = query.Where(c => c.CoachId == user.Coach.Id);
            }
            else
            {
                query = query.Where(c => c.UserId == appUserId);
            }

            return await query.ToListAsync(cancellationToken);
        }


    }
}
