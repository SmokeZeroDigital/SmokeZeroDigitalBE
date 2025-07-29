using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs;

namespace SmokeZeroDigitalProject.Pages.ProgressEntry
{
    public class CreateModel : PageModel
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly ApiConfig _apiConfig;

        [BindProperty]
        public CreateProgressEntryDto ProgressDto { get; set; } = new()
        {
            EntryDate = DateTime.Now
        };

        public CreateModel(IHttpClientFactory clientFactory, ApiConfig apiConfig)
        {
            _clientFactory = clientFactory;
            _apiConfig = apiConfig;
        }

        public void OnGet()
        {
            if (ProgressDto.EntryDate == DateTime.MinValue)
                ProgressDto.EntryDate = DateTime.Now;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var http = _clientFactory.CreateClient("ApiClient");

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userIdStr) && Guid.TryParse(userIdStr, out var userId))
            {
                ProgressDto.UserId = userId;
            }
            else
            {
                ModelState.AddModelError("", "Không tìm thấy UserId trong phiên làm việc. Vui lòng đăng nhập lại.");
                return Page();
            }

            if (ProgressDto.EntryDate == DateTime.MinValue)
                ProgressDto.EntryDate = DateTime.Now;

            var response = await http.PostAsJsonAsync(
                _apiConfig.GetEndpoint(ApiEndpoints.CreateProgressEntry), ProgressDto);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Created progress entry successfully.";
                return RedirectToPage("/ProgressEntry/Index", new { userId = ProgressDto.UserId });
            }

            ModelState.AddModelError(string.Empty, "Failed to create progress entry.");
            return Page();
        }
    }
}