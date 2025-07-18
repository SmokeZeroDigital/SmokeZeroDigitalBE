using SmokeZeroDigitalSolution.Application.Features.Chat.Queries;
using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Queries;
using SmokeZeroDigitalSolution.Contracts.Conversation;
using SmokeZeroDigitalSolution.Contracts.Noti;
using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : BaseApiController
    {
        private readonly IRequestExecutor _executor;

        public CoachController(ISender sender, IRequestExecutor executor) : base(sender)
        {
            _executor = executor;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCoachRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<CreateCoachRequest, CoachResponseDto>(
                 request,
                 req => new CreateCoachCommand
                 {
                     Coach = new CreateCoachDto
                     {
                         UserId = req.UserId,
                         Bio = req.Bio,
                         Specialization = req.Specialization,
                         Rating = req.Rating
                     }
                 },
                 nameof(Create),
                 cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCoachRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<UpdateCoachRequest, CoachResponseDto>(
                request,
                req => new UpdateCoachCommand
                {
                    Id = id,
                    Coach = new UpdateCoachDto
                    {
                        Bio = req.Bio,
                        Specialization = req.Specialization,
                        IsAvailable = req.IsAvailable
                    }
                },
                nameof(Update),
                cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<Guid, CoachResponseDto>(
                id,
                _ => new GetCoachByIdQuery { Id = id },
                nameof(GetById),
                cancellationToken);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<object, CoachListResponseDto>(
                null,
                _ => new GetAllCoachesQuery(),
                nameof(GetAll),
                cancellationToken);
        }
        [HttpGet("list-user")]
        public async Task<IActionResult> GetUserByCoach([FromBody] GetListCoachesOrUser request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GetListCoachesOrUser, List<UserInfoDto>>(
                request,
                req => new GetUsersByCoachIdQuery
                {
                    CoachId = req.Id
                },

                nameof(GetUserByCoach),
                cancellationToken);
        }
    }
}