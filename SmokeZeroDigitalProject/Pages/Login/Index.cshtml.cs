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

        public void OnGet(string? email)
        {
            // Pre-fill email if user is returning from email confirmation
            if (!string.IsNullOrEmpty(email))
            {
                LoginRequest.UserName = email;
            }
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
            
            try
            {
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
                    // Check if the error is due to unconfirmed email
                    var errorContent = await response.Content.ReadAsStringAsync();
                    
                    // Try to parse the error response
                    try
                    {
                        var options = new System.Text.Json.JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var errorResult = System.Text.Json.JsonSerializer.Deserialize<ApiErrorResult>(errorContent, options);
                        
                        if (errorResult?.Error != null && errorResult.Error.InnerException != null && errorResult.Error.InnerException.StartsWith("EMAIL_NOT_CONFIRMED:"))
                        {
                            // Parse the error message format: "EMAIL_NOT_CONFIRMED:userId:email:message"
                            var parts = errorResult.Error.InnerException.Split(':', 4);
                            if (parts.Length >= 4)
                            {
                                var userId = parts[1];
                                var email = parts[2];
                                var message = parts[3];
                                
                                TempData["ToastMessage"] = "info:Email chưa được xác nhận. Mã OTP mới đã được gửi đến email của bạn.";
                                return RedirectToPage("/ConfirmEmail/Index", new { 
                                    email = email,
                                    userId = userId
                                });
                            }
                        }
                    }
                    catch
                    {
                        // Fallback parsing for simple error messages
                        if (errorContent.Contains("Email not confirmed") || errorContent.Contains("new confirmation code has been sent"))
                        {
                            var email = LoginRequest.UserName;
                            TempData["ToastMessage"] = "info:Email chưa được xác nhận. Mã OTP mới đã được gửi đến email của bạn.";
                            return RedirectToPage("/ConfirmEmail/Index", new { email = email });
                        }
                    }
                    
                    TempData["ToastMessage"] = "error:Tên đăng nhập hoặc mật khẩu không đúng.";
                    return Page();
                }
            }
            catch (HttpRequestException)
            {
                TempData["ToastMessage"] = "error:Không thể kết nối đến server. Vui lòng thử lại.";
                return Page();
            }
            catch (Exception)
            {
                TempData["ToastMessage"] = "error:Đã xảy ra lỗi trong quá trình đăng nhập. Vui lòng thử lại.";
                return Page();
            }
        }
    }
}