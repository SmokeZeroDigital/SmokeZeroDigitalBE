using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Commands
{
    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, CommandResult<FeedbackResponseDto>>
    {
        private readonly IFeedbackService _feedbackService;

        public CreateFeedbackCommandHandler(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<CommandResult<FeedbackResponseDto>> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _feedbackService.CreateFeedbackAsync(request.Feedback, cancellationToken);
                return CommandResult<FeedbackResponseDto>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<FeedbackResponseDto>.Failure(ex.Message);
            }
        }
    }
}
