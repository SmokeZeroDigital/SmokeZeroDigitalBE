using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmokeZeroDigitalProject.Pages.Admin.Plan
{
    public class CreateModel : PageModel
    {

		private readonly ApiConfig _apiConfig;

		public CreateModel(IConfiguration configuration)
		{
			_apiConfig = new ApiConfig(configuration);
		}
		[BindProperty]
		public CreatePlanRequest CreatePlanRequest { get; set; } = new();
		public async Task<IActionResult> OnPostAsync()
        {
			if (string.IsNullOrWhiteSpace(CreatePlanRequest.Name) ||
	             CreatePlanRequest.Price <= 0 ||
	             CreatePlanRequest.DurationInDays <= 0)
			{
				TempData["ToastMessage"] = "error:Vui lòng điền đầy đủ và hợp lệ các thông tin của gói.";
				return Page();
			}

			var createUrl = _apiConfig.GetEndpoint(ApiEndpoints.CreatePlan);

			using var httpClient = new HttpClient();
			var response = await httpClient.PostAsJsonAsync(createUrl, CreatePlanRequest);

			if (response.IsSuccessStatusCode)
			{
				TempData["ToastMessage"] = "success:Tạo gói đăng ký thành công!";
				return RedirectToPage("Index");
			}
			else
			{
				var errorMsg = await response.Content.ReadAsStringAsync();
				TempData["ToastMessage"] = $"error:Tạo gói thất bại. {errorMsg}";
				return Page();
			}
		}
    }
}
