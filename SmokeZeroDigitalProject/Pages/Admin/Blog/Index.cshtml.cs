using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs;
using System.Text;

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
		public string? Message { get; set; }

		public async Task<IActionResult> OnPostDeleteAsync(Guid id)
		{
			var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.DeleteBlog);

			using var httpClient = new HttpClient();
			var deletePayload = new DeleteUserRequest { UserId = id };

			var json = JsonSerializer.Serialize(deletePayload);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await httpClient.DeleteAsync(apiUrl);

			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Delete,
				RequestUri = new Uri(apiUrl),
				Content = content
			};

			var deleteResponse = await httpClient.SendAsync(request);

			if (deleteResponse.IsSuccessStatusCode)
			{
				Message = "Xoá thành công";
			}
			else
			{
				Message = "Lỗi xoá bài đăng";
			}

			return RedirectToPage();
		}
	}
}
