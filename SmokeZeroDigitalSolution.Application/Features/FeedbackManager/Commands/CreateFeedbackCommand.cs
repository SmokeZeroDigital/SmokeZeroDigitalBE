using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Commands
{
    public class CreateFeedbackCommand : IRequest<CommandResult<FeedbackResponseDto>>
    {
        public CreateFeedbackDto Feedback { get; init; } = default!;
    }
    
    
}
