using SmokeZeroDigitalSolution.Application.Features.CommentManager.Commands;
using SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Validators
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentValidator()
        {
            RuleFor(c => c.Comment.UserId)
                .NotEmpty().WithMessage("UserId is required.");
            RuleFor(c => c.Comment.Content)
                .NotEmpty().WithMessage("Content is required.")
                .Length(1, 1000).WithMessage("Content must be between 1 and 1000 characters.");
            RuleFor(x => x)
             .Must(x => x.Comment.PostId.HasValue || x.Comment.ArticleId.HasValue)
             .WithMessage("Either PostId or ArticleId must be provided.");
        }
    
    }
}
