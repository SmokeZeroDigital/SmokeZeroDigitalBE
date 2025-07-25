
using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Commands
{
    public class CreateSmokingRecordCommandHandler : IRequestHandler<CreateSmokingRecordCommand, CommandResult<SmokingRecordDto>>
    {
        private readonly ISmokingRecordService smokingRecordService;
        private readonly IUnitOfWork unitOfWork;
        public CreateSmokingRecordCommandHandler(ISmokingRecordService smokingRecordService, IUnitOfWork unitOfWork)
        {
            this.smokingRecordService = smokingRecordService;
            this.unitOfWork = unitOfWork;
        }
        public async Task<CommandResult<SmokingRecordDto>> Handle(CreateSmokingRecordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await smokingRecordService.CreateAsync(request.Record);
                await unitOfWork.SaveAsync(cancellationToken);
                return CommandResult<SmokingRecordDto>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<SmokingRecordDto>.Failure(ex.Message);
            }
        }
    }
}
