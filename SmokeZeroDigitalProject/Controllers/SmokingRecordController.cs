using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Commands;
using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Queries;

namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmokingRecordController : BaseApiController
    {
        private readonly IRequestExecutor _executor;

        public SmokingRecordController(ISender sender, IRequestExecutor executor) : base(sender)
        {
            _executor = executor;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateSmokingRecordDto request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<CreateSmokingRecordDto, SmokingRecordDto>(
                request,
                req => new CreateSmokingRecordCommand { Record = req },
                nameof(Create),
                cancellationToken
            );
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GetAllSmokingRecordsQuery, List<SmokingRecordDto>>(
                new GetAllSmokingRecordsQuery(),
                _ => new GetAllSmokingRecordsQuery(),
                nameof(GetAll),
                cancellationToken
            );
        }

        [HttpGet("by-user/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GetSmokingRecordsByUserIdQuery, List<SmokingRecordDto>>(
                new GetSmokingRecordsByUserIdQuery { UserId = userId },
                _ => new GetSmokingRecordsByUserIdQuery { UserId = userId },
                nameof(GetByUserId),
                cancellationToken
            );
        }
    }
}
