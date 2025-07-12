using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface;

namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.Commands
{
    public class UpdateNotiCommandHandler : IRequestHandler<UpdateNotiCommand, CommandResult<Notification>>
    {
        private readonly INotiService _notiService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNotiCommandHandler(INotiService notiService, IUnitOfWork unitOfWork)
        {
            _notiService = notiService;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandResult<Notification>> Handle(UpdateNotiCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _notiService.UpdateNotiAsync(request.Dto);
                await _unitOfWork.SaveAsync(cancellationToken);
                return CommandResult<Notification>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<Notification>.Failure(ex.Message);
            }
        }
    }
}
