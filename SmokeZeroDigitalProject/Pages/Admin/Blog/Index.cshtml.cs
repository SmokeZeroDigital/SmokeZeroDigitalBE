using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs;

namespace SmokeZeroDigitalProject.Pages.Admin.Blog
{
    public class IndexModel : PageModel
    {
		private readonly ApiConfig _apiConfig;

		public IndexModel(IConfiguration configuration)
		{
			_apiConfig = new ApiConfig(configuration);
		}
		public List<BlogReponseDto> Blogs { get; set; } = new();


		public async Task OnGetAsync()
		{
			var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetAllBlogs);

			using var httpClient = new HttpClient();
			var response = await httpClient.GetAsync(apiUrl);

			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

				var apiResult = System.Text.Json.JsonSerializer.Deserialize<ApiSuccessResult<List<BlogReponseDto>>>(responseBody, options);
				Blogs = apiResult?.Content ?? new List<BlogReponseDto>();
			}
		}
	}
}
