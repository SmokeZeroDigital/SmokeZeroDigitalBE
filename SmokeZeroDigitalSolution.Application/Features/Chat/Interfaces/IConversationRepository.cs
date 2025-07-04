namespace SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces
{
    public interface IConversationRepository
    {
        Task<Conversation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Conversation?> GetByUserAndCoachAsync(Guid userId, Guid coachId, CancellationToken cancellationToken = default);
        Task<Conversation?> GetByIdWithParticipantsAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(Conversation conversation, CancellationToken cancellationToken = default);
        Task UpdateAsync(Conversation conversation, CancellationToken cancellationToken = default);

    }
}
