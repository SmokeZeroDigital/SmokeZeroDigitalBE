using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Commands;
using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Queries;
using SmokeZeroDigitalSolution.Contracts.Feedback;
namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : BaseApiController
    {
        private readonly IRequestExecutor _executor;

        public FeedbackController(ISender sender, IRequestExecutor executor) : base(sender)
        {
            _executor = executor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<CreateFeedbackRequest, FeedbackResponseDto>(
                request,
                req => new CreateFeedbackCommand
                {
                    Feedback = new CreateFeedbackDto
                    {
                        UserId = req.UserId,
                        CoachId = req.CoachId,
                        Content = req.Content,
                        Rating = req.Rating
                    }
                },
                nameof(CreateFeedback),
                cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetFeedbackByIdRequest { Id = id };

            return await _executor.ExecuteQueryAsync<GetFeedbackByIdRequest, FeedbackResponseDto>(
                request,
                req => new FeedbackQueryById
                {
                    Id = req.Id
                },
                nameof(GetFeedbackById),
                cancellationToken);
        }
        [HttpGet("by-coach/{id}")]
        public async Task<IActionResult> GetFeedbackByCoach([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetFeedbackByIdRequest { Id = id };
            return await _executor.ExecuteQueryAsync<GetFeedbackByIdRequest, IEnumerable<FeedbackResponseDto>>(
                request,
                req => new FeedbackQueryByCoach
                {
                    CoachId = req.Id
                },
                nameof(GetFeedbackByCoach),
                cancellationToken);
        }

    }
}