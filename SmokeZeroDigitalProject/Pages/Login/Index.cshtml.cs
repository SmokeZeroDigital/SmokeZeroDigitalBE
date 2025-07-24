using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;

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

        public async Task OnPostGoogleLogin()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = Url.Page("/Login/Index", "GoogleResponse") // Sử dụng Url.Page thay vì Url.Action
            });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!result.Succeeded)
            {
                TempData["ToastMessage"] = "error:Xác thực Google thất bại.";
                return RedirectToPage("/Login/Index");
            }
            var idToken = await HttpContext.GetTokenAsync("id_token");
            if (string.IsNullOrEmpty(idToken))
            {
                TempData["ToastMessage"] = "error:Không thể lấy token từ Google.";
                return RedirectToPage("/Login/Index");
            }
            var loginUrl = _apiConfig.GetEndpoint(ApiEndpoints.GoogleLogin);
            using var httpClient = new HttpClient();
            var googleLoginRequest = new GoogleLoginRequest
            {
                Token = idToken
            };
            var response = await httpClient.PostAsJsonAsync(loginUrl, googleLoginRequest);
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
                return RedirectToPage("/Login/Index");
            }
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