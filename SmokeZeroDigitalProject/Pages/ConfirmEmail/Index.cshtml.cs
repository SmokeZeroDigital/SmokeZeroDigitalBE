using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokeZeroDigitalProject.Helpers;
using SmokeZeroDigitalSolution.Contracts.Auth;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;
using System.Text.Json;

namespace SmokeZeroDigitalProject.Pages.ConfirmEmail
{
    public class IndexModel : PageModel
    {
        private readonly ApiConfig _apiConfig;

        public IndexModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }

        [BindProperty]
        public string OtpCode { get; set; } = string.Empty;

        public string? Email { get; set; }
        public Guid UserId { get; set; }

        public void OnGet(string? email, string? userId)
        {
            Email = email;
            if (!string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out var parsedUserId))
            {
                UserId = parsedUserId;
            }
        }

        public async Task<IActionResult> OnPostAsync(string? email, string? userId)
        {
            Email = email;
            if (!string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out var parsedUserId))
            {
                UserId = parsedUserId;
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(OtpCode))
            {
                TempData["ToastMessage"] = "error:Vui lòng nhập mã OTP.";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                TempData["ToastMessage"] = "error:Thông tin email không hợp lệ.";
                return Page();
            }

            // Basic OTP format validation
            if (OtpCode.Length != 6 || !OtpCode.All(char.IsDigit))
            {
                TempData["ToastMessage"] = "error:Mã OTP phải là 6 chữ số.";
                return Page();
            }

            try
            {
                var confirmEmailUrl = _apiConfig.GetEndpoint(ApiEndpoints.ConfirmEmail);
                
                // Check if we have a valid UserId
                if (UserId != Guid.Empty)
                {
                    // Use the real API call with the UserId
                    var request = new ConfirmEmailRequest
                    {
                        UserId = UserId,
                        Token = OtpCode
                    };

                    using var httpClient = new HttpClient();
                    var response = await httpClient.PostAsJsonAsync(confirmEmailUrl, request);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize<ConfirmEmailResultDto>(responseContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (result?.Success == true)
                        {
                            TempData["ToastMessage"] = "success:Email đã được xác nhận thành công! Bạn có thể đăng nhập ngay bây giờ.";
                            return RedirectToPage("/Login/Index");
                        }
                        else
                        {
                            TempData["ToastMessage"] = $"error:{result?.Message ?? "Xác nhận email thất bại."}";
                            return Page();
                        }
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        TempData["ToastMessage"] = "error:Mã OTP không hợp lệ hoặc đã hết hạn.";
                        return Page();
                    }
                }
                else
                {
                    // Fallback: Use email-based confirmation when UserId is not available
                    var confirmEmailByEmailUrl = _apiConfig.GetEndpoint(ApiEndpoints.ConfirmEmailByEmail);
                    var emailRequest = new ConfirmEmailByEmailRequest
                    {
                        Email = Email,
                        Token = OtpCode
                    };

                    using var httpClient = new HttpClient();
                    var response = await httpClient.PostAsJsonAsync(confirmEmailByEmailUrl, emailRequest);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize<ConfirmEmailResultDto>(responseContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (result?.Success == true)
                        {
                            TempData["ToastMessage"] = "success:Email đã được xác nhận thành công! Bạn có thể đăng nhập ngay bây giờ.";
                            return RedirectToPage("/Login/Index");
                        }
                        else
                        {
                            TempData["ToastMessage"] = $"error:{result?.Message ?? "Xác nhận email thất bại."}";
                            return Page();
                        }
                    }
                    else
                    {
                        TempData["ToastMessage"] = "error:Mã OTP không hợp lệ hoặc đã hết hạn.";
                        return Page();
                    }
                }
            }
            catch (HttpRequestException)
            {
                TempData["ToastMessage"] = "error:Không thể kết nối đến server. Vui lòng thử lại.";
                return Page();
            }
            catch (Exception)
            {
                TempData["ToastMessage"] = "error:Đã xảy ra lỗi trong quá trình xác nhận email. Vui lòng thử lại.";
                return Page();
            }
        }
    }
}
