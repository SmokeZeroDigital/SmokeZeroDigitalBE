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
                PlanDto.StartDate = DateTime.Now;
            if (PlanDto.ExpectedEndDate == DateTime.MinValue)
                PlanDto.ExpectedEndDate = DateTime.Now.AddDays(30);
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
                ModelState.AddModelError("", "Không tìm thấy UserId trong phiên làm việc. Vui lòng đăng nhập lại.");
                return Page();
            }

            if (PlanDto.StartDate == DateTime.MinValue)
                PlanDto.StartDate = DateTime.Now;
            if (PlanDto.ExpectedEndDate == DateTime.MinValue)
                PlanDto.ExpectedEndDate = DateTime.Now.AddDays(30);

            var response = await http.PostAsJsonAsync(
                _apiConfig.GetEndpoint(ApiEndpoints.CreateQuittingPlan), PlanDto);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Created quitting plan successfully.";
                return RedirectToPage("/QuittingPlan/Index", new { userId = PlanDto.UserId });
            }

            ModelState.AddModelError(string.Empty, "Failed to create plan.");
            return Page();
        }
    }
}