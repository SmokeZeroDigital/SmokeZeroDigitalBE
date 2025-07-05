using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Commands;

namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Validators
{
    public class CreateFeedbackValidator : AbstractValidator<CreateFeedbackCommand>
    {
        public CreateFeedbackValidator()
        {
            RuleFor(x => x.Feedback.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.Feedback.CoachId).NotEmpty().WithMessage("CoachId is required.");
            RuleFor(x => x.Feedback.Content).NotEmpty().MaximumLength(1000).WithMessage("Content is required and must not exceed 1000 characters.");
            RuleFor(x => x.Feedback.Rating).InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
        }
    }
}
