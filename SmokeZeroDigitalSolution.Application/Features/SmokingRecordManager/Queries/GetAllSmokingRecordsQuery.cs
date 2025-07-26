using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Queries
{
    public class GetAllSmokingRecordsQuery : IRequest<QueryResult<List<SmokingRecordDto>>> { }

}
