using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokeZeroDigitalProject.Helpers;
using System.Text.Json;

namespace SmokeZeroDigitalProject.Pages.Payment.Success
{
    public class IndexModel : PageModel
    {
        private readonly ApiConfig _apiConfig;

        public IndexModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }

        public string Status { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;
        public string OrderInfo { get; set; } = string.Empty;

        public async Task OnGetAsync(string status, string amount, string orderInfo = "")
        {
            Status = status ?? string.Empty;
            Amount = amount ?? string.Empty;
            OrderInfo = orderInfo ?? string.Empty;

            // Debug: Kiểm tra sessions hiện tại
            var fullName = HttpContext.Session.GetString("FullName");
            var planId = HttpContext.Session.GetString("PlanId");
            var userId = HttpContext.Session.GetString("UserId");
            var selectedCoachId = HttpContext.Session.GetString("SelectedCoachId");

            // Chỉ xóa các session tạm thời, KHÔNG xóa session quan trọng
            if (status == "success")
            {
                // Tạo conversation giữa user và coach nếu có đủ thông tin
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(selectedCoachId))
                {
                    await CreateConversationAsync(userId, selectedCoachId);
                }

                HttpContext.Session.Remove("SelectedPlanId");
                HttpContext.Session.Remove("SelectedCoachId");

                // Đảm bảo các session quan trọng vẫn còn
                if (string.IsNullOrEmpty(fullName))
                {
                    // Có thể session bị mất, cần restore từ database hoặc redirect về login
                    // Tạm thời để debug
                }
            }
        }

        private async Task CreateConversationAsync(string userId, string coachId)
        {
            try
            {
                var requestBody = new
                {
                    appUserId = userId,
                    coachId = coachId
                };

                var conversationUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetOrCreateConversation);
                using var httpClient = new HttpClient();
                var response = await httpClient.PostAsJsonAsync(conversationUrl, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    // Conversation đã được tạo thành công
                    // Có thể log hoặc xử lý response nếu cần
                }
            }
            catch (Exception ex)
            {
                // Log exception nếu cần
                // Không throw để không ảnh hưởng đến flow chính
            }
        }
    }
}
