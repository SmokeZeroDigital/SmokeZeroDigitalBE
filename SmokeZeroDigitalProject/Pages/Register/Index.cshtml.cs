using System.Text.Json;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;

namespace SmokeZeroDigitalProject.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly ApiConfig _apiConfig;

        public IndexModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }

        [BindProperty]
        public RegisterRequest RegisterRequest { get; set; } = new();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(RegisterRequest.Email) ||
                string.IsNullOrWhiteSpace(RegisterRequest.Username) ||
                string.IsNullOrWhiteSpace(RegisterRequest.Password) ||
                string.IsNullOrWhiteSpace(RegisterRequest.ConfirmPassword) ||
                string.IsNullOrWhiteSpace(RegisterRequest.FullName))
            {
                TempData["ToastMessage"] = "error:Vui lòng điền đầy đủ các trường bắt buộc.";
                return Page();
            }

            if (RegisterRequest.Password != RegisterRequest.ConfirmPassword)
            {
                TempData["ToastMessage"] = "error:Mật khẩu không khớp.";
                return Page();
            }

            var registerUrl = _apiConfig.GetEndpoint(ApiEndpoints.Register);

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(registerUrl, RegisterRequest);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    // Get the response content to extract userId
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var registerResult = JsonSerializer.Deserialize<RegisterResultDto>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (registerResult != null)
                    {
                        TempData["ToastMessage"] = "success:Vui lòng nhập mã OTP để hoàn tất đăng ký!";
                        TempData["ConfirmEmail"] = RegisterRequest.Email;
                        
                        // Pass both email and userId to the ConfirmEmail page
                        return RedirectToPage("/ConfirmEmail/Index", new { 
                            email = RegisterRequest.Email, 
                            userId = registerResult.UserId.ToString() 
                        });
                    }
                    else
                    {
                        TempData["ToastMessage"] = "success:Vui lòng nhập mã OTP để hoàn tất đăng ký!";
                        return RedirectToPage("/ConfirmEmail/Index", new { email = RegisterRequest.Email });
                    }
                }
                catch (Exception)
                {
                    // Fallback if JSON parsing fails
                    TempData["ToastMessage"] = "success:Vui lòng nhập mã OTP để hoàn tất đăng ký!";
                    return RedirectToPage("/ConfirmEmail/Index", new { email = RegisterRequest.Email });
                }
            }
            else
            {
                TempData["ToastMessage"] = "error:Đăng ký thất bại. Email hoặc Username có thể đã tồn tại.";
                return Page();
            }
        }
    }
}