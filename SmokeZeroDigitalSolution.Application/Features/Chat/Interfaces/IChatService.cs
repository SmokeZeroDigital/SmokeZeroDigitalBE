using SmokeZeroDigitalSolution.Application.Features.Chat.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces
{
    public interface IChatService
    {
        Task<ChatMessageDto> SendMessageAsync(SendMessageDto dto, CancellationToken cancellationToken);

    }
}
