using SmokeZeroDigitalSolution.Application.Features.BlogManager.Interfaces;
using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface;

namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.Commands
{
    public class CreateNotiCommandHandler : IRequestHandler<CreateNotiCommand, CommandResult<Notification>>
    {
        private readonly INotiService _notiService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateNotiCommandHandler(INotiService notiService, IUnitOfWork unitOfWork)
        {
            _notiService = notiService;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandResult<Notification>> Handle(CreateNotiCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _notiService.CreateNotiAsync(request.Dto);
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
