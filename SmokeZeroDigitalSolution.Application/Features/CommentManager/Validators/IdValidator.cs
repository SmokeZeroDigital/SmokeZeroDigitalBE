using SmokeZeroDigitalSolution.Application.Features.CommentManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Validators
{
    public class IdValidator<T> : AbstractValidator<T> where T : IHasId
    {
        public IdValidator()
        {
            RuleFor(x => x.Id)
           .NotEmpty().WithMessage("CommentId is required.");
        }
    }
    
}
