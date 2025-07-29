using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokeZeroDigitalProject.Helpers;
using SmokeZeroDigitalProject.Common.Models;
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
                        
                        // The confirm-email endpoint returns ApiSuccessResult<ConfirmEmailResultDto>
                        // So we need to deserialize the wrapper first
                        var apiResult = JsonSerializer.Deserialize<ApiSuccessResult<ConfirmEmailResultDto>>(responseContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        
                        var result = apiResult?.Content;
                        
                        if (result?.Success == true)
                        {
                            // Automatically log the user in after successful email confirmation
                            await SetUserSessionAsync();
                            
                            TempData["ToastMessage"] = "success:Email đã được xác nhận thành công! Chào mừng bạn đến với SmokeZero Digital!";
                            return RedirectToPage("/Index");
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
                            // Automatically log the user in after successful email confirmation
                            await SetUserSessionAsync();
                            
                            TempData["ToastMessage"] = "success:Email đã được xác nhận thành công! Chào mừng bạn đến với SmokeZero Digital!";
                            return RedirectToPage("/Index");
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

        private async Task SetUserSessionAsync()
        {
            try
            {
                // After successful email confirmation, we need to establish a proper user session
                // Since we don't have password, we'll call a special endpoint or simulate a successful login
                
                // For now, let's try to get user information and create a session
                // We'll set the session similar to how the login page does it
                var userEmail = Email ?? "";
                
                // Try to get more user information if we have a UserId
                if (UserId != Guid.Empty)
                {
                    // Use the UserId to get user information
                    await GetUserInfoAndSetSession(UserId);
                }
                else
                {
                    // Fallback: set basic session with email
                    HttpContext.Session.SetString("FullName", userEmail);
                    HttpContext.Session.SetString("PlanId", "");
                    HttpContext.Session.SetString("UserId", "");
                }
            }
            catch (Exception)
            {
                // Fallback to basic session with email
                HttpContext.Session.SetString("FullName", Email ?? "");
                HttpContext.Session.SetString("PlanId", "");
                HttpContext.Session.SetString("UserId", UserId.ToString());
            }
        }

        private async Task GetUserInfoAndSetSession(Guid userId)
        {
            try
            {
                // Call the User API to get user information
                var getUserUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetUserById.Replace("{userId}", userId.ToString()));
                
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(getUserUrl);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Parse the user information and set session
                    // For now, we'll use a simplified approach
                    var userInfo = System.Text.Json.JsonSerializer.Deserialize<dynamic>(responseContent);
                    
                    // Set session data similar to login
                    HttpContext.Session.SetString("FullName", Email ?? "");
                    HttpContext.Session.SetString("PlanId", ""); // Will be updated when user info is available
                    HttpContext.Session.SetString("UserId", userId.ToString());
                }
                else
                {
                    // Fallback if API call fails
                    HttpContext.Session.SetString("FullName", Email ?? "");
                    HttpContext.Session.SetString("PlanId", "");
                    HttpContext.Session.SetString("UserId", userId.ToString());
                }
            }
            catch (Exception)
            {
                // Fallback to basic session
                HttpContext.Session.SetString("FullName", Email ?? "");
                HttpContext.Session.SetString("PlanId", "");
                HttpContext.Session.SetString("UserId", userId.ToString());
            }
        }

        private Task SetSessionWithUserLookup()
        {
            // This is a simplified approach - in a real scenario, you might want to 
            // create a dedicated API endpoint to get user info by email after confirmation
            // For now, we'll set the session with the available data
            HttpContext.Session.SetString("FullName", Email ?? "");
            HttpContext.Session.SetString("PlanId", "");
            HttpContext.Session.SetString("UserId", UserId != Guid.Empty ? UserId.ToString() : "");
            
            return Task.CompletedTask;
        }
    }
}
