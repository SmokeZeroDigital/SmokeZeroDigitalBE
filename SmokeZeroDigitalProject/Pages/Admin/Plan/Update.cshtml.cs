using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmokeZeroDigitalProject.Pages.Admin.Plan
{
    public class UpdateModel : PageModel
    {

		private readonly ApiConfig _apiConfig;

		public UpdateModel(IConfiguration configuration)
		{
			_apiConfig = new ApiConfig(configuration);
		}
		[BindProperty]
		public UpdatePlanDto UpdatePlanDto { get; set; } = new();
		public async Task<IActionResult> OnGetAsync(Guid guid)
		{
			var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetPlan) + $"?id={guid}";


            using var httpClient = new HttpClient();
			var response = await httpClient.GetAsync(apiUrl);

			if (!response.IsSuccessStatusCode)
			{
				TempData["ToastMessage"] = "error:Không tìm thấy gói đăng ký.";
				return RedirectToPage("Index");
			}

			var result = await response.Content.ReadFromJsonAsync<UpdatePlanDto>();
			if (result != null)
			{
				UpdatePlanDto = result;
			}

			return Page();
		}
		public async Task<IActionResult> OnPostAsync(Guid id)
		{
			if (string.IsNullOrWhiteSpace(UpdatePlanDto.Name) ||
				UpdatePlanDto.Price <= 0 ||
				UpdatePlanDto.DurationInDays <= 0)
			{
				TempData["ToastMessage"] = "error:Vui lòng điền đầy đủ và hợp lệ các thông tin của gói.";
				return Page();
			}

			var updateUrl = _apiConfig.GetEndpoint(ApiEndpoints.UpdatePlan) + $"/{id}";

			using var httpClient = new HttpClient();
			var response = await httpClient.PutAsJsonAsync(updateUrl, UpdatePlanDto);

			if (response.IsSuccessStatusCode)
			{
				TempData["ToastMessage"] = "success:Cập nhật gói đăng ký thành công!";
				return RedirectToPage("Index");
			}
			else
			{
				var errorMsg = await response.Content.ReadAsStringAsync();
				TempData["ToastMessage"] = $"error:Cập nhật thất bại. {errorMsg}";
				return Page();
			}
		}
	}
}
