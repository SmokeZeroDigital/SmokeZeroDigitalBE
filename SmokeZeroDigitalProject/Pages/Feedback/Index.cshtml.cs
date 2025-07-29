using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmokeZeroDigitalProject.Pages.Feedback
{
    public class IndexModel : PageModel
    {
        private readonly ApiConfig _apiConfig;
        public IndexModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }
        public IEnumerable<FeedbackResponseDto> Feedbacks { get; set; } = Enumerable.Empty<FeedbackResponseDto>();


        [BindProperty(SupportsGet = true)]
        public string CoachId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            HttpContext.Session.SetString("UserId", "48ea93bf-b8a6-4c08-a1c9-c153f5af5703");
            CoachId = HttpContext.Session.GetString("UserId") ?? string.Empty;

            if (CoachId == null)
                return BadRequest("Coach ID is required.");
            
            var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetFeedbackByCoachId).Replace("{coachId}", CoachId.ToString());
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var apiResult = System.Text.Json.JsonSerializer.Deserialize<ApiSuccessResult<List<FeedbackResponseDto>>>(responseBody, options);
                Feedbacks = apiResult?.Content ?? new List<FeedbackResponseDto>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to load feedbacks.");
            }

            return Page();
        }
    }
}
