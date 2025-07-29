using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;

namespace SmokeZeroDigitalProject.Pages.SmokingRecord
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApiConfig _apiConfig;

        [BindProperty]
        public CreateSmokingRecordDto RecordDto { get; set; } = new()
        {
            RecordDate = DateTime.Now
        };

        public CreateModel(IHttpClientFactory clientFactory, ApiConfig apiConfig)
        {
            _clientFactory = clientFactory;
            _apiConfig = apiConfig;
        }

        public void OnGet()
        {
            if (RecordDto.RecordDate == DateTime.MinValue)
                RecordDto.RecordDate = DateTime.Now;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var http = _clientFactory.CreateClient("ApiClient");

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userIdStr) && Guid.TryParse(userIdStr, out var userId))
            {
                RecordDto.UserId = userId;
            }
            else
            {
                ModelState.AddModelError("", "Không tìm thấy UserId trong phiên làm việc. Vui lòng đăng nhập lại.");
                return Page();
            }

            if (RecordDto.RecordDate == DateTime.MinValue)
                RecordDto.RecordDate = DateTime.Now;

            var response = await http.PostAsJsonAsync(
                _apiConfig.GetEndpoint(ApiEndpoints.CreateSmokingRecord), RecordDto);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Created smoking record successfully.";
                return RedirectToPage("/SmokingRecord/Index", new { userId = RecordDto.UserId });
            }

            ModelState.AddModelError(string.Empty, "Failed to create smoking record.");
            return Page();
        }
    }
}