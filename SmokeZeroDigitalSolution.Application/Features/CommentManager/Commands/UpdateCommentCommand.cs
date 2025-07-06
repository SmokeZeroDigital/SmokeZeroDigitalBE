using SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Commands
{
    public class UpdateCommentCommand : IRequest<CommandResult<CommentDto>>
    {
        public UpdateCommentDto Comment { get; init; } = default!;
    }
}
