using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmokeZeroDigitalProject.Pages.Notification
{
    public class IndexModel : PageModel
    {
        private readonly ApiConfig _apiConfig;
        public IndexModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }
        public List<SmokeZeroDigitalSolution.Domain.Entites.Notification> Notifications { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            //check role from session

            var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetAllNotis); 
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var apiResult = System.Text.Json.JsonSerializer.Deserialize<ApiSuccessResult<List<SmokeZeroDigitalSolution.Domain.Entites.Notification>>>(responseBody, options);
                Notifications = apiResult?.Content ?? new List<SmokeZeroDigitalSolution.Domain.Entites.Notification>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to load notifications.");
            }

            return Page();
        }
    }
}
