using SmokeZeroDigitalSolution.Application.Features.BlogManager.Commands;
using SmokeZeroDigitalSolution.Application.Features.BlogManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Commands
{
    public class IncreaseViewCommandHandler : IRequestHandler<IncreaseViewCommand, CommandResult<int>>
    {
        private readonly IBlogService _blogService;
        private readonly IUnitOfWork _unitOfWork;
        public IncreaseViewCommandHandler(IBlogService blogService, IUnitOfWork unitOfWork)
        {
            _blogService = blogService;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandResult<int>> Handle(IncreaseViewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogService.IncreaseViewCountAsync(request.BlogId);
                await _unitOfWork.SaveAsync(cancellationToken);
                return CommandResult<int>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<int>.Failure(ex.Message);
            }
        }
    }

}
