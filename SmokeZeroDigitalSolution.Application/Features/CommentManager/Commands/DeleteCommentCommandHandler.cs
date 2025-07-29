namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Commands
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, CommandResult<bool>>
    {
        private readonly ICommentService _commentService;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommentCommandHandler(ICommentService commentService, IUnitOfWork unitOfWork)
        {
            _commentService = commentService;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandResult<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentService.GetByIdAsync(request.Id);
            if (comment == null || comment.IsDeleted)
            {
                return CommandResult<bool>.Failure("Comment not found or already deleted.");
            }
            comment.IsDeleted = true; // Mark as deleted
            await _unitOfWork.SaveAsync(cancellationToken);
            return CommandResult<bool>.Success(true);
        }
    }
    
}
