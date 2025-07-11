using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface;

namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.Commands
{
    public class DeleteNotiCommandHandler : IRequestHandler<DeleteNotiCommand, CommandResult<bool>>
    {
        private readonly INotiService _notiService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNotiCommandHandler(INotiService notiService, IUnitOfWork unitOfWork)
        {
            _notiService = notiService;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandResult<bool>> Handle(DeleteNotiCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _notiService.DeleteNotificationAsync(request.Id);
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
