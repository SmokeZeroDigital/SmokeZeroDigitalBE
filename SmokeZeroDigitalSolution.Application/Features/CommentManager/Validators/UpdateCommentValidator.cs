using SmokeZeroDigitalSolution.Application.Features.CommentManager.Commands;

namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Validators
{
    public class UpdateCommentValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentValidator()
        {
            RuleFor(x => x.Comment.Id)
            .NotEmpty().WithMessage("Comment Id is required.");

            RuleFor(x => x.Comment.Content)
                .NotEmpty().WithMessage("Content is required.")
                .Length(1, 1000).WithMessage("Content must be between 1 and 1000 characters.");
        }
    }
}
