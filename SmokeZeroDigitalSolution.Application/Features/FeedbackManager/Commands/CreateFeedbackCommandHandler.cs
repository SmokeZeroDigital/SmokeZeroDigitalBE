using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Commands
{
    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, CommandResult<FeedbackResponseDto>>
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFeedbackCommandHandler(IFeedbackService feedbackService, IUnitOfWork unitOfWork)
        {
            _feedbackService = feedbackService;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult<FeedbackResponseDto>> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _feedbackService.CreateFeedbackAsync(request.Feedback);
                await _unitOfWork.SaveAsync(cancellationToken);
                return CommandResult<FeedbackResponseDto>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<FeedbackResponseDto>.Failure(ex.Message);
            }
        }
    }
}
