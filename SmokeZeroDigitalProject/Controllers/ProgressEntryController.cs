
using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Commands;
using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Queries;

namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressEntryController : BaseApiController
    {
        private readonly IRequestExecutor _executor;

        public ProgressEntryController(ISender sender, IRequestExecutor executor) : base(sender)
        {
            _executor = executor;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProgressEntryDto request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<CreateProgressEntryDto, ProgressEntryDto>(
                request,
                req => new CreateProgressEntryCommand { Entry = req },
                nameof(Create),
                cancellationToken
            );
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GetAllProgressEntriesQuery, List<ProgressEntryDto>>(
                new GetAllProgressEntriesQuery(),
                _ => new GetAllProgressEntriesQuery(),
                nameof(GetAll),
                cancellationToken
            );
        }

        [HttpGet("by-user/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GetProgressEntriesByUserIdQuery, List<ProgressEntryDto>>(
                new GetProgressEntriesByUserIdQuery { UserId = userId },
                _ => new GetProgressEntriesByUserIdQuery { UserId = userId },
                nameof(GetByUserId),
                cancellationToken
            );
        }
    }
}