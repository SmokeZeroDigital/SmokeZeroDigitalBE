using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokeZeroDigitalSolution.Domain.Entites;
using System.Text;

namespace SmokeZeroDigitalProject.Pages.Admin.User
{
    public class IndexModel : PageModel
    {
		private readonly ApiConfig _apiConfig;

		public IndexModel(IConfiguration configuration)
		{
			_apiConfig = new ApiConfig(configuration);
		}

		public List<AppUser> Users { get; set; } = new();

		public async Task OnGetAsync()
		{
			var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetAllUser);

			using var httpClient = new HttpClient();
			var response = await httpClient.GetAsync(apiUrl);

			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

				var apiResult = System.Text.Json.JsonSerializer.Deserialize<ApiSuccessResult<List<AppUser>>>(responseBody, options);
				Users = apiResult?.Content ?? new List<AppUser>();
			}
		}
		public string? Message { get; set; }

		public async Task<IActionResult> OnPostDeleteAsync(Guid id)
		{
			var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.DeleteUser);

			var deletePayload = new DeleteUserRequest { UserId = id };
			var json = JsonSerializer.Serialize(deletePayload);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			using var httpClient = new HttpClient();

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
				Message = "Lỗi xoá người dùng";
			}

			return RedirectToPage();
		}
	}
}
