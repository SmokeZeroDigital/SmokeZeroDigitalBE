namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentPlanController : BaseApiController
    {
        private readonly IRequestExecutor _executor;

        public PaymentPlanController(ISender sender, IRequestExecutor executor) : base(sender)
        {
            _executor = executor;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody] CreatePlanRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<CreatePlanRequest, CreatePlanResultDto>(
                request,
                req => new RegisterPlanCommand
                {
                    Plan = new CreatePlanDTO
                    {
                        Name = req.Name,
                        Description = req.Description,
                        Price = req.Price,
                        DurationInDays = req.DurationInDays,
                    }
                },
               
                nameof(CreatePlan),
                cancellationToken);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlan([FromQuery] GetPlanRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<GetPlanRequest, GetPlanResponseDto>(
                request,
                req => new GetPlanQuery
                {
                    Plan = new GetPlanDto
                    {
                        Id = req.Id
                    }
                  
                },
                nameof(GetPlan),
                cancellationToken);
        }
    }
}
