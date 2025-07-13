
using SmokeZeroDigitalSolution.Application.Features.BlogManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Commands
{
	public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, CommandResult<bool>>
	{
		private readonly IBlogService _blogService;
		private readonly IUnitOfWork _unitOfWork;

		public DeleteBlogCommandHandler(IBlogService _blogService, IUnitOfWork unitOfWork)
		{
			_blogService = _blogService;
			_unitOfWork = unitOfWork;
		}
		public async Task<CommandResult<bool>> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
		{
			try
			{
				await _blogService.DeleteBlogAsync(request.Id);
				await _unitOfWork.SaveAsync(cancellationToken);
				return CommandResult<bool>.Success(true);
			}
			catch (Exception ex)
			{
				return CommandResult<bool>.Failure(ex.Message);
			}
		}
	}
}
