using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.VNPay;

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

        [HttpPost("payment-url")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] VNPayRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<VNPayRequest, PaymentResponseModel>(
                 request,
                 req => new VNPayCommand
                 {
                     Payment = new PaymentInformationModel
                     {
                         UserId = req.UserId,
                         SubscriptionPlanId = req.SubscriptionPlanId,
                         Name = req.Name,
                         Amount = req.Amount,
                         OrderDescription = req.OrderDescription,
                         OrderType = req.OrderType
                     },
                 },
                 nameof(CreatePaymentUrl),
                 cancellationToken);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> VNPayCallback(CancellationToken cancellationToken)
        {
            var result = await _executor.ExecuteAsync<VNPayCallbackRequest, VNPayCallbackResultDto>(
                new VNPayCallbackRequest { Query = Request.Query },
                req => new VNPayCallbackCommand { Query = req.Query },
                nameof(VNPayCallback),
                cancellationToken);

            if (result is ObjectResult objectResult && objectResult.Value is CommandResult<VNPayCallbackResultDto> commandResult)
            {
                var callbackResult = commandResult.Content;
                var successUrl = "https://your-frontend.com/payment-success";
                var failedUrl = "https://your-frontend.com/payment-failed";

                if (callbackResult != null && callbackResult.IsSuccess)
                {
                    return Redirect($"{successUrl}?status=success&amount={callbackResult.Amount}");
                }
                else
                {
                    return Redirect($"{failedUrl}?status=failed&amount={callbackResult?.Amount ?? 0}");
                }
            }

            return BadRequest();
        }

    }
}
