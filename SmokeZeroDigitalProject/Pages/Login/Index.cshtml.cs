namespace SmokeZeroDigitalProject.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly ApiConfig _apiConfig;

        public LoginModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }

        [BindProperty]
        public LoginRequest LoginRequest { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(LoginRequest.UserName) || string.IsNullOrWhiteSpace(LoginRequest.Password))
            {
                TempData["ToastMessage"] = "error:Vui lòng nhập tên đăng nhập và mật khẩu.";
                return Page();
            }

            var loginUrl = _apiConfig.GetEndpoint(ApiEndpoints.Login);
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(loginUrl, LoginRequest);

            if (response.IsSuccessStatusCode)
            {
                var responseBodyAsString = await response.Content.ReadAsStringAsync();
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResult = System.Text.Json.JsonSerializer.Deserialize<ApiSuccessResult<AuthResponseDto>>(responseBodyAsString, options);

                var fullName = apiResult?.Content?.UserName;
                var userId = apiResult?.Content?.UserId;
                var planId = apiResult?.Content?.PlanId;
                if (string.IsNullOrWhiteSpace(fullName))
                    fullName = apiResult?.Content?.UserName ?? apiResult?.Content?.UserName ?? "";
                
                HttpContext.Session.SetString("FullName", fullName);
                HttpContext.Session.SetString("PlanId", planId?.ToString() ?? string.Empty);
                HttpContext.Session.SetString("UserId", userId.ToString() ?? string.Empty);
                TempData["ToastMessage"] = "success:Đăng nhập thành công!";
                return RedirectToPage("/Index");
            }
            else
            {
                TempData["ToastMessage"] = "error:Tên đăng nhập hoặc mật khẩu không đúng.";
                return Page();
            }
        }
    }
}