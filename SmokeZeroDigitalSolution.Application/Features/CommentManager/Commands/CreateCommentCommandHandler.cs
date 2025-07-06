using SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.CommentManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Commands
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommandResult<CommentDto>>
    {
        private readonly ICommentService _commentService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCommentCommandHandler(ICommentService commentService, IUnitOfWork unitOfWork)
        {
            _commentService = commentService;   
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandResult<CommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commentService.AddAsync(request.Comment);
                await _unitOfWork.SaveAsync(cancellationToken);
                return CommandResult<CommentDto>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<CommentDto>.Failure(ex.Message);
            }
        }
    }
   
    
}
