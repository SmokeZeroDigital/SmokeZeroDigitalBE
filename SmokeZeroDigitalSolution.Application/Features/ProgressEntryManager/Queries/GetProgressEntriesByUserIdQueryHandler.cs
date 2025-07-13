using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Queries
{
    public class GetProgressEntriesByUserIdQueryHandler : IRequestHandler<GetProgressEntriesByUserIdQuery, QueryResult<List<ProgressEntryDto>>>
    {
        private readonly IProgressEntryService _progressEntryService;
        public GetProgressEntriesByUserIdQueryHandler(IProgressEntryService progressEntryService)
        {
            _progressEntryService = progressEntryService;
        }
        public Task<QueryResult<List<ProgressEntryDto>>> Handle(GetProgressEntriesByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _progressEntryService.GetByUserIdAsync(request.UserId);
                return Task.FromResult(QueryResult<List<ProgressEntryDto>>.Success(result.Result));
            }
            catch (Exception ex)
            {
                return Task.FromResult(QueryResult<List<ProgressEntryDto>>.Failure(ex.Message));
            }
        }
    }
}
