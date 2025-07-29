namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Commands
{
    public class CreateCommentCommand : IRequest<CommandResult<CommentDto>>
    {
        public CreateCommentDto Comment { get; init; } = default!;
    }
}
