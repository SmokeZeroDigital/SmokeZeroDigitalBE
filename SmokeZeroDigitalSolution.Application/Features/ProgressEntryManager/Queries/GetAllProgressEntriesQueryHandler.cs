using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Queries
{
    public class GetAllProgressEntriesQueryHandler : IRequestHandler<GetAllProgressEntriesQuery, QueryResult<List<ProgressEntryDto>>>
    {
        private readonly IProgressEntryService _progressEntryService;
        public GetAllProgressEntriesQueryHandler(IProgressEntryService progressEntryService)
        {
            _progressEntryService = progressEntryService;
        }
        public Task<QueryResult<List<ProgressEntryDto>>> Handle(GetAllProgressEntriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _progressEntryService.GetAllAsync();
                return Task.FromResult(QueryResult<List<ProgressEntryDto>>.Success(result.Result));
            }
            catch (Exception ex)
            {
                return Task.FromResult(QueryResult<List<ProgressEntryDto>>.Failure(ex.Message));
            }
        }
    }
}
