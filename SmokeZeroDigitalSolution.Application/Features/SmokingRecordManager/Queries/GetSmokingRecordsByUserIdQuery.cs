using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Queries
{
    public class GetSmokingRecordsByUserIdQuery : IRequest<CommandResult<List<SmokingRecordDto>>>
    {
        public Guid UserId { get; set; }
    }

}
