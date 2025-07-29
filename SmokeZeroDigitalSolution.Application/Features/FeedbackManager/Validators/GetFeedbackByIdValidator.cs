namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Validators
{
    public class GetFeedbackByIdValidator : AbstractValidator<FeedbackQueryById>
    {
        public GetFeedbackByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.");
        }
    }
}
