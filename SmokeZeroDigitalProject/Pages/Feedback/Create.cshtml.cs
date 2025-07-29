using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace SmokeZeroDigitalProject.Pages.Feedback
{
    public class CreateModel : PageModel
    {
        private readonly ApiConfig _apiConfig;

        public CreateModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }

        [BindProperty]
        public CreateFeedbackDto Feedback { get; set; } = new();

        public List<CoachResponseDto> Coaches { get; set; } = new();

        public SelectList CoachSelectList => new(Coaches, "Id", "FullName");

        public async Task<IActionResult> OnGetAsync()
        {
            var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetAllCoach);

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var apiResult = JsonSerializer.Deserialize<ApiSuccessResult<CoachListResponseDto>>(responseBody, options);
                Coaches = apiResult?.Content?.Data ?? new List<CoachResponseDto>();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return Page();
            }
            HttpContext.Session.SetString("UserId", "f3f63f9a-41e4-4445-9e00-11d11f86acc3");
            Feedback.UserId = Guid.Parse(HttpContext.Session.GetString("UserId") ?? string.Empty);
            var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.CreateFeedback);
            using var httpClient = new HttpClient();

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(Feedback),
                Encoding.UTF8,
                "application/json"
            );

            var response = await httpClient.PostAsync(apiUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error.");
                return Page();
            }

            TempData["SuccessMessage"] = "Success";
            return RedirectToPage(); 
        }

    }
}
