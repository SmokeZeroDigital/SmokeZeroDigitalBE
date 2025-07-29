using SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.DTOs;

namespace SmokeZeroDigitalProject.Pages.QuittingPlan
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApiConfig _apiConfig;

        [BindProperty]
        public CreateQuittingPlanDto PlanDto { get; set; } = new();

        public CreateModel(IHttpClientFactory clientFactory, ApiConfig apiConfig)
        {
            _clientFactory = clientFactory;
            _apiConfig = apiConfig;
        }

        public void OnGet()
        {
            if (PlanDto.StartDate == DateTime.MinValue)
                PlanDto.StartDate = DateTime.Today;

            if (PlanDto.ExpectedEndDate == DateTime.MinValue)
                PlanDto.ExpectedEndDate = DateTime.Today.AddDays(30);

            if (PlanDto.ExpectedEndDate < DateTime.Today)
            {
                ViewData["Warning"] = "⚠️ Ngày kết thúc đã qua. Đây sẽ là kế hoạch quá khứ.";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var http = _clientFactory.CreateClient("ApiClient");

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userIdStr) && Guid.TryParse(userIdStr, out var userId))
            {
                PlanDto.UserId = userId;
            }
            else
            {
                ModelState.AddModelError("", "Không tìm thấy UserId trong session. Vui lòng đăng nhập lại.");
                return Page();
            }

            if (PlanDto.ExpectedEndDate < DateTime.Today)
            {
                ViewData["Warning"] = "Bạn đang tạo kế hoạch có ngày kết thúc trong quá khứ.";
            }

            var response = await http.PostAsJsonAsync(
                _apiConfig.GetEndpoint(ApiEndpoints.CreateQuittingPlan), PlanDto);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Tạo kế hoạch cai thuốc thành công.";
                return RedirectToPage("/QuittingPlan/Index", new { userId = PlanDto.UserId });
            }

            ModelState.AddModelError(string.Empty, "Tạo kế hoạch thất bại. Vui lòng thử lại.");
            return Page();
        }
    }
}