using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Queries
{
    public class GetSmokingRecordsByUserIdQuery : IRequest<QueryResult<List<SmokingRecordDto>>>
    {
        public Guid UserId { get; set; }
    }

}
