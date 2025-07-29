namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories;

public class ChatMessageRepository : IChatMessageRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public ChatMessageRepository(ApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public void Add(ChatMessage message)
    {
        _context.ChatMessages.Add(message);
        _unitOfWork.Save();
    }

    public async Task<ChatMessage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ChatMessages.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ChatMessage>> GetByConversationAsync(Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await _context.ChatMessages
            .Include(m => m.User)
            .Include(m => m.Coach).ThenInclude(c => c.User)
            .Where(m => m.ConversationId == conversationId)
            .OrderBy(m => m.Timestamp)
            .ToListAsync(cancellationToken);
    }

}
