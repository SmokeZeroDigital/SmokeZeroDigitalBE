using SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Commands
{
    public class CreateBlogCommand : IRequest<CommandResult<BlogArticle>>
    {
        public CreateBlogDto CreateBlogDto { get; init; } = default!;
    }
}
