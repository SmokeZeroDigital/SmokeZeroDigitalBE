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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPlans(CancellationToken cancellationToken)
        {
            return await _executor.ExecuteQueryAsync<object, List<GetPlanResponseDto>>(
                null,
                _ => new GetAllPlansQuery(),
                nameof(GetAllPlans),
                cancellationToken
            );
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
            try
            {
                // Lấy thông tin từ VNPay callback
                var responseCode = Request.Query["vnp_ResponseCode"].ToString();
                var transactionStatus = Request.Query["vnp_TransactionStatus"].ToString();
                var amount = Request.Query["vnp_Amount"].ToString();
                var orderInfo = Request.Query["vnp_OrderInfo"].ToString();

                // Log debug info
                var queryParams = Request.Query.ToDictionary(x => x.Key, x => x.Value.ToString());
                var queryString = string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));

                bool isSuccess = responseCode == "00" && transactionStatus == "00";

                if (isSuccess)
                {
                    try
                    {
                        var result = await _executor.ExecuteAsync<VNPayCallbackRequest, VNPayCallbackResultDto>(
                            new VNPayCallbackRequest { Query = Request.Query },
                            req => new VNPayCallbackCommand { Query = req.Query },
                            nameof(VNPayCallback),
                            cancellationToken);
                    }
                    catch (Exception)
                    {
                        // Log exception nếu cần
                    }

                    // Parse orderInfo để lấy planId: trong trường hợp này chỉ có planId
                    if (Guid.TryParse(orderInfo, out var planId))
                    {
                        // Cập nhật session PlanId để button hiển thị đúng
                        HttpContext.Session.SetString("PlanId", planId.ToString());

                        // Tạm thời hardcode user info - trong thực tế cần lấy từ database
                        // dựa vào payment transaction hoặc session trước đó
                        if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                        {
                            // Giả sử có một user mặc định hoặc lấy từ payment record
                            HttpContext.Session.SetString("UserId", "default-user-id");
                            HttpContext.Session.SetString("FullName", "User");
                        }
                    }
                    else
                    {
                        // Fallback: thử parse như format cũ
                        var orderParts = orderInfo.Split('|');

                        if (orderParts.Length >= 1 && Guid.TryParse(orderParts[0], out planId))
                        {
                            HttpContext.Session.SetString("PlanId", planId.ToString());
                        }

                        if (orderParts.Length >= 3 && Guid.TryParse(orderParts[2], out var userId))
                        {
                            HttpContext.Session.SetString("UserId", userId.ToString());
                            HttpContext.Session.SetString("FullName", "User");
                        }
                    }

                    // Chuyển đổi amount từ VNPay format (đã nhân 100) về format bình thường
                    var displayAmount = !string.IsNullOrEmpty(amount) && long.TryParse(amount, out var amountValue)
                        ? (amountValue / 100).ToString()
                        : "0";

                    return Redirect($"/Payment/Success?status=success&amount={displayAmount}&orderInfo={Uri.EscapeDataString(orderInfo)}");
                }
                else
                {
                    // Thanh toán thất bại
                    var displayAmount = !string.IsNullOrEmpty(amount) && long.TryParse(amount, out var amountValue)
                        ? (amountValue / 100).ToString()
                        : "0";

                    return Redirect($"/Payment/Failed?status=failed&amount={displayAmount}&responseCode={responseCode}&orderInfo={Uri.EscapeDataString(orderInfo)}");
                }
            }
            catch (Exception ex)
            {
                // Log exception và redirect về failed page
                return Redirect($"/Payment/Failed?status=error&amount=0&error={Uri.EscapeDataString(ex.Message)}");
            }
        }

    }
}
