using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Queries
{
    public class GetAllSmokingRecordsQueryHandler : IRequestHandler<GetAllSmokingRecordsQuery, QueryResult<List<SmokingRecordDto>>>
    {
        private readonly ISmokingRecordService _smokingRecordService;
        public GetAllSmokingRecordsQueryHandler(ISmokingRecordService smokingRecordService)
        {
            _smokingRecordService = smokingRecordService;
        }
        public async Task<QueryResult<List<SmokingRecordDto>>> Handle(GetAllSmokingRecordsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _smokingRecordService.GetAllAsync();
                return QueryResult<List<SmokingRecordDto>>.Success(result);
            }
            catch
            {
                return QueryResult<List<SmokingRecordDto>>.Failure("An error occurred while retrieving smoking records.");
            }
        }
    }

}
