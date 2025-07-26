using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Commands
{
    public class CreateSmokingRecordCommand : IRequest<CommandResult<SmokingRecordDto>>
    {
        public CreateSmokingRecordDto Record { get; set; } = default!;
    }

}
