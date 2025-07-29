using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Queries
{
    public class GetAllProgressEntriesQuery : IRequest<QueryResult<List<ProgressEntryDto>>>
    {
    }
}
