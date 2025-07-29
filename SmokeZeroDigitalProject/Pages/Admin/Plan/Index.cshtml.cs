using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalProject.Pages.Admin.Plan
{
    public class IndexModel : PageModel
    {
		private readonly ApiConfig _apiConfig;

		public IndexModel(IConfiguration configuration)
		{
			_apiConfig = new ApiConfig(configuration);
		}
		public List<GetPlanResponseDto> Plans { get; set; } = new();


		public async Task OnGetAsync()
		{
			var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetAllPlan);

			using var httpClient = new HttpClient();
			var response = await httpClient.GetAsync(apiUrl);

			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

				var apiResult = System.Text.Json.JsonSerializer.Deserialize<ApiSuccessResult<List<GetPlanResponseDto>>>(responseBody, options);
				Plans = apiResult?.Content ?? new List<GetPlanResponseDto>();
			}
		}
	}
}
