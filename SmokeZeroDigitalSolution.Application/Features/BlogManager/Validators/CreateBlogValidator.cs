using SmokeZeroDigitalSolution.Application.Features.BlogManager.Commands;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Validators
{
    public class CreateBlogValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogValidator() 
        {
     
            // Title validation (matches [Required] + [StringLength(200)])
            RuleFor(x => x.CreateBlogDto.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters")
                .Must(BeValidTitle).WithMessage("Title contains invalid characters");

            // Content validation (matches [Required])
            RuleFor(x => x.CreateBlogDto.Content)
                .NotEmpty().WithMessage("Content is required")
                .MinimumLength(20).WithMessage("Content should be at least 20 characters")
                .MaximumLength(10000).WithMessage("Content is too long");

            // Author validation (matches [Required])
            RuleFor(x => x.CreateBlogDto.AuthorUserId)
                .NotEmpty().WithMessage("Author is required")
                .Must(BeValidGuid).WithMessage("Invalid author ID format");

            // Tags validation (matches [StringLength(500)])
            RuleFor(x => x.CreateBlogDto.Tags)
                .MaximumLength(500).WithMessage("Tags must not exceed 500 characters")
                .When(x => !string.IsNullOrEmpty(x.CreateBlogDto.Tags))
                .Must(BeValidTagsFormat).WithMessage("Tags must be comma-separated values");
        }

        private bool BeValidTitle(string title)
        {
            // Custom title validation logic
            return !string.IsNullOrWhiteSpace(title) &&
                   !title.StartsWith(" ") &&
                   !title.EndsWith(" ");
        }

        private bool BeValidGuid(Guid guid)
        {
            return guid != Guid.Empty;
        }

        private bool BeValidTagsFormat(string? tags)
        {
            if (string.IsNullOrEmpty(tags)) return true;

            var tagArray = tags.Split(',');
            return tagArray.All(tag =>
                !string.IsNullOrWhiteSpace(tag.Trim()) &&
                tag.Trim().Length <= 50 &&
                !tag.Contains("<")); // Basic HTML tag prevention
        }
    }
    
}
