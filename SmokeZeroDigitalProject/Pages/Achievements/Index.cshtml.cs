using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmokeZeroDigitalProject.Pages.Achievements
{
    public class IndexModel : PageModel
    {
        public List<UserAchievement> UserAchievements { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            UserAchievements = new List<UserAchievement>
        {
            new UserAchievement
            {
                AchievedDate = DateTime.Today.AddDays(-7),
                Achievement = new Achievement
                {
                    Title = "1 Tuần Không Hút Thuốc",
                    Description = "Bạn đã không hút thuốc trong 7 ngày liên tiếp! Chúc mừng nha^^",
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/190/190411.png",
                    BadgeColor = "#28a745"
                }
            },
            new UserAchievement
            {
                AchievedDate = DateTime.Today.AddDays(-30),
                Achievement = new Achievement
                {
                    Title = "1 Tháng Tự Do",
                    Description = "Bạn Đã Kiên Trì 1 tháng không hút thuốc!",
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/1035/1035688.png",
                    BadgeColor = "#17a2b8"
                }
            },
            new UserAchievement
            {
                AchievedDate = DateTime.Today.AddDays(-1),
                Achievement = new Achievement
                {
                    Title = "Ngày Không Khói",
                    Description = "Thêm 1 ngày bỏ thuốc, thật yomost!",
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/190/190411.png",
                    BadgeColor = "#ffc107"
                }
            }
        };
            return Page();
        }

        public class UserAchievement
        {
            public Achievement Achievement { get; set; } = new();
            public DateTime AchievedDate { get; set; }
        }

        public class Achievement
        {
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string IconUrl { get; set; } = string.Empty;
            public string BadgeColor { get; set; } = "#6c757d";
        }
    }
}