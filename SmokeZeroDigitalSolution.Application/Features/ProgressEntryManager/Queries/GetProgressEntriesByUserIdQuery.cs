using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Queries
{
    public class GetProgressEntriesByUserIdQuery : IRequest<QueryResult<ProgressEntryDto>>
    {
        public Guid UserId { get; set; }
    }

}
