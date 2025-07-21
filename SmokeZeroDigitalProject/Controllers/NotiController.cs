using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Commands;
using SmokeZeroDigitalSolution.Application.Features.NotificationManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Queries;
using SmokeZeroDigitalSolution.Contracts.Noti;
using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotiController : BaseApiController
    {
        private readonly IRequestExecutor _executor;
        public NotiController(ISender sender, IRequestExecutor executor) : base(sender)
        {
            _executor = executor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNoti([FromBody] CreateNotiRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<CreateNotiRequest, Notification>(
                request,
                req => new CreateNotiCommand
                {
                    Dto = new CreateNotiDto
                    {
                        UserId = req.UserId,
                        Title = req.Title,
                        Type = req.Type,
                        Message = req.Message,
                        ScheduledTime = req.ScheduledTime,
                        RecurrencePattern = req.RecurrencePattern
                    }
                },
                nameof(CreateNoti),
                cancellationToken);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNoti([FromBody] UpdateNotiRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<UpdateNotiRequest, Notification>(
                request,
                req => new UpdateNotiCommand
                {
                    Dto = new CreateNotiDto
                    {
                        UserId = req.Id,
                        Title = req.Title,
                        Type = req.Type,
                        Message = req.Message,
                        ScheduledTime = req.ScheduledTime,
                        RecurrencePattern = req.RecurrencePattern
                    }
                },
                nameof(UpdateNoti),
                cancellationToken);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNoti([FromBody] DeleteNotiRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<DeleteNotiRequest, bool>(
                request,
                req => new DeleteNotiCommand
                {
                    Id = req.Id,
                },
                nameof(DeleteNoti),
                cancellationToken);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNoti(CancellationToken cancellationToken)
        {
            var request = new GetAllNotiRequest();
            return await _executor.ExecuteQueryAsync<GetAllNotiRequest, IQueryable<Notification>>(
                request,
                req => new GetAllNoti(),
                nameof(GetAllNoti),
                cancellationToken);
        }

        [HttpGet("by-id")]
        public async Task<IActionResult> GetById([FromBody] NotiRequestById request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<NotiRequestById, Notification>(
                request,
                req => new GetNotiById
                {
                    Id = req.Id
                },
                nameof(GetById),
                cancellationToken);
        }

        [HttpGet("by-user")]
        public async Task<IActionResult> GetByUserId([FromBody] NotiRequestById request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<NotiRequestById, IEnumerable<Notification>>(
                request,
                req => new GetNotiByUser
                {
                    UserId = req.Id
                },
                nameof(GetById),
                cancellationToken);
        }
    }
}
