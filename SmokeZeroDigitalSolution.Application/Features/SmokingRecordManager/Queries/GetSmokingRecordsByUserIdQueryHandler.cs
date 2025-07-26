using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Queries
{
    public class GetSmokingRecordsByUserIdQueryHandler : IRequestHandler<GetSmokingRecordsByUserIdQuery, QueryResult<List<SmokingRecordDto>>>
    {
        private readonly ISmokingRecordService _smokingRecordService;
        public GetSmokingRecordsByUserIdQueryHandler(ISmokingRecordService smokingRecordService)
        {
            _smokingRecordService = smokingRecordService;
        }
        public async Task<QueryResult<List<SmokingRecordDto>>> Handle(GetSmokingRecordsByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _smokingRecordService.GetByUserIdAsync(request.UserId);
                return QueryResult<List<SmokingRecordDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<List<SmokingRecordDto>>.Failure(ex.Message);
            }
        }
    }
}
