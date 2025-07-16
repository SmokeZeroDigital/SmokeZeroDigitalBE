namespace SmokeZeroDigitalProject.Pages.Payment.Checkout
{
    public class IndexModel : PageModel
    {
        private readonly ApiConfig _apiConfig;

        public IndexModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }

        public GetPlanResponseDto? SelectedPlan { get; set; }
        public CoachResponseDto? SelectedCoach { get; set; }
        public string? SelectedPlanId { get; set; }
        public string? SelectedCoachId { get; set; }
        public string? UserId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Lấy thông tin từ session
            SelectedPlanId = HttpContext.Session.GetString("SelectedPlanId");
            SelectedCoachId = HttpContext.Session.GetString("SelectedCoachId");
            UserId = HttpContext.Session.GetString("UserId");

            // Kiểm tra đủ thông tin
            if (string.IsNullOrEmpty(SelectedPlanId) || string.IsNullOrEmpty(SelectedCoachId) || string.IsNullOrEmpty(UserId))
            {
                TempData["ToastMessage"] = "error:Vui lòng hoàn thành các bước trước đó!";
                return RedirectToPage("/Payment/Plan");
            }

            // Lấy thông tin plan
            await LoadPlanInfo();

            // Lấy thông tin coach
            await LoadCoachInfo();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Lấy thông tin từ session
                SelectedPlanId = HttpContext.Session.GetString("SelectedPlanId");
                SelectedCoachId = HttpContext.Session.GetString("SelectedCoachId");
                UserId = HttpContext.Session.GetString("UserId");

                if (string.IsNullOrEmpty(SelectedPlanId) || string.IsNullOrEmpty(SelectedCoachId) || string.IsNullOrEmpty(UserId))
                {
                    TempData["ToastMessage"] = "error:Thông tin không đầy đủ!";
                    return RedirectToPage("/Payment/Plan");
                }

                // Load plan info to get price
                await LoadPlanInfo();

                if (SelectedPlan == null)
                {
                    TempData["ToastMessage"] = "error:Không tìm thấy thông tin gói đăng ký!";
                    return RedirectToPage("/Payment/Plan");
                }

                // Tạo request để gọi API VNPay
                var vnPayRequest = new VNPayRequest
                {
                    UserId = Guid.Parse(UserId),
                    SubscriptionPlanId = Guid.Parse(SelectedPlanId),
                    Amount = (double)SelectedPlan.Price,
                    OrderType = "subscription",
                    OrderDescription = $"{SelectedPlanId}|{SelectedCoachId}|{UserId}|Thanh toán gói {SelectedPlan.Name}",
                    Name = SelectedPlan.Name
                };

                var paymentUrl = _apiConfig.GetEndpoint(ApiEndpoints.CreatePaymentUrl);
                using var httpClient = new HttpClient();
                var response = await httpClient.PostAsJsonAsync(paymentUrl, vnPayRequest);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var apiResult = JsonSerializer.Deserialize<JsonElement>(responseBody, options);

                    // Lấy token từ API result để redirect đến VNPay
                    if (apiResult.TryGetProperty("content", out var content) &&
                        content.TryGetProperty("token", out var tokenElement))
                    {
                        var token = tokenElement.GetString();
                        if (!string.IsNullOrEmpty(token))
                        {
                            return Redirect(token);
                        }
                    }
                }

                TempData["ToastMessage"] = "error:Không thể tạo liên kết thanh toán!";
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = $"error:Có lỗi xảy ra: {ex.Message}";
                return Page();
            }
        }

        private async Task LoadPlanInfo()
        {
            if (!string.IsNullOrEmpty(SelectedPlanId))
            {
                var planUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetPlan) + $"?id={SelectedPlanId}";
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(planUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var apiResult = JsonSerializer.Deserialize<ApiSuccessResult<GetPlanResponseDto>>(responseBody, options);
                    SelectedPlan = apiResult?.Content;
                }
            }
        }

        private async Task LoadCoachInfo()
        {
            if (!string.IsNullOrEmpty(SelectedCoachId))
            {
                var coachUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetCoachById.Replace("{id}", SelectedCoachId));
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(coachUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var apiResult = JsonSerializer.Deserialize<ApiSuccessResult<CoachResponseDto>>(responseBody, options);
                    SelectedCoach = apiResult?.Content;
                }
            }
        }
    }
}
