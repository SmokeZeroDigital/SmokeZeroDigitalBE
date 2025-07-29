using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmokeZeroDigitalProject.Pages
{
    public class PrivacyModel : PageModel
    {
		private readonly ApiConfig _apiConfig;

		public PrivacyModel(IConfiguration configuration)
		{
			_apiConfig = new ApiConfig(configuration);
		}
		public List<FeedbackResponseDto> Feedbacks { get; set; } = new();


		public async Task OnGetAsync()
		{
			var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetAllBlogs);

			using var httpClient = new HttpClient();
			var response = await httpClient.GetAsync(apiUrl);

			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

				var apiResult = System.Text.Json.JsonSerializer.Deserialize<ApiSuccessResult<List<FeedbackResponseDto>>>(responseBody, options);
				Feedbacks = apiResult?.Content ?? new List<FeedbackResponseDto>();
			}
		}
	}

}
