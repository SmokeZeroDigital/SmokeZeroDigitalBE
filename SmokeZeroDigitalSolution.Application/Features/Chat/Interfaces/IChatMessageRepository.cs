namespace SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces
{
    public interface IChatMessageRepository
    {
        void Add(ChatMessage message);
        Task<ChatMessage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ChatMessage>> GetByConversationAsync(Guid conversationId, CancellationToken cancellationToken = default);
    }
}
