using SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.Queries;
using SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.Commands;
using SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.DTOs;

namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuittingPlanController : BaseApiController
    {
        private readonly IRequestExecutor _executor;

        public QuittingPlanController(ISender sender, IRequestExecutor executor) : base(sender)
        {
            _executor = executor;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateQuittingPlanDto request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<CreateQuittingPlanDto, QuittingPlanDto>(
                request,
                req => new CreateQuittingPlanCommand { QuittingPlan = req },
                nameof(Create),
                cancellationToken
            );
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GetAllQuittingPlanQuery, List<QuittingPlanDto>>(
                new GetAllQuittingPlanQuery(),
                _ => new GetAllQuittingPlanQuery(),
                nameof(GetAll),
                cancellationToken
            );
        }

        [HttpGet("by-user/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GetQuittingPlanByUserIdQuery, QuittingPlanDto>(
                new GetQuittingPlanByUserIdQuery { UserId = userId },
                _ => new GetQuittingPlanByUserIdQuery { UserId = userId },
                nameof(GetByUserId),
                cancellationToken
            );
        }
    }
}
