using SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Commands
{
    public class UpdateBlogCommand : IRequest<CommandResult<BlogArticle>>
    {
        public UpdateBlogDto Dto { get; init; } = default!;
    }
}
