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
                         QuotationId = req.QuotationId,
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
        public IActionResult PaymentCallback()
        {
            try
            {
                var response = _vnPayService.PaymentExecute(Request.Query);
                var amount = Request.Query["vnp_Amount"];
                var actualAmount = int.Parse(amount) / 100;
                var successUrl = configuration["FrontendRedirect:SuccessUrl"];
                var failedUrl = configuration["FrontendRedirect:FailedUrl"];
                if (Request.Query["vnp_ResponseCode"] == "00")
                {
                    return Redirect($"{successUrl}?status=success&amount={actualAmount}");
                }
                else
                {
                    return Redirect($"{failedUrl}?status=failed&amount={actualAmount}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
