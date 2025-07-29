namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Commands
{
    public class DeleteCommentCommand : IRequest<CommandResult<bool>>, IHasId
    {
        public Guid Id { get; init; } = default!;
    }
}
