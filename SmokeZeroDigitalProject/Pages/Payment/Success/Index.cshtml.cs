using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmokeZeroDigitalProject.Pages.Payment.Success
{
    public class IndexModel : PageModel
    {
        public string Status { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;
        public string OrderInfo { get; set; } = string.Empty;

        public void OnGet(string status, string amount, string orderInfo = "")
        {
            Status = status ?? string.Empty;
            Amount = amount ?? string.Empty;
            OrderInfo = orderInfo ?? string.Empty;

            // Debug: Kiểm tra sessions hiện tại
            var fullName = HttpContext.Session.GetString("FullName");
            var planId = HttpContext.Session.GetString("PlanId");
            var userId = HttpContext.Session.GetString("UserId");

            // Chỉ xóa các session tạm thời, KHÔNG xóa session quan trọng
            if (status == "success")
            {
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
    }
}
