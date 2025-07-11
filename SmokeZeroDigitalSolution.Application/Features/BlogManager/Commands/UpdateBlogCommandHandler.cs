using SmokeZeroDigitalSolution.Application.Features.BlogManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Commands
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, CommandResult<BlogArticle>>
    {
        private readonly IBlogService _blogService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBlogCommandHandler(IBlogService blogService, IUnitOfWork unitOfWork)
        {
            _blogService = blogService;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandResult<BlogArticle>> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogService.UpdateAsync(request.Dto);
                await _unitOfWork.SaveAsync(cancellationToken);
                return CommandResult<BlogArticle>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<BlogArticle>.Failure(ex.Message);
            }
        }
    }

}
