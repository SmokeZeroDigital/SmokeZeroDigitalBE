using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmokeZeroDigitalProject.Pages.Achievements
{
    public class IndexModel : PageModel
    {
        public List<UserAchievement> UserAchievements { get; set; } = new();

        public void OnGet()
        {
            // Hardcoded data
            UserAchievements = new List<UserAchievement>
        {
            new UserAchievement
            {
                AchievedDate = DateTime.Today.AddDays(-7),
                Achievement = new Achievement
                {
                    Title = "1 Tu?n Kh�ng H�t Thu?c",
                    Description = "B?n ?� kh�ng h�t thu?c trong 7 ng�y li�n ti?p!",
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/190/190411.png",
                    BadgeColor = "#28a745"
                }
            },
            new UserAchievement
            {
                AchievedDate = DateTime.Today.AddDays(-30),
                Achievement = new Achievement
                {
                    Title = "1 Th�ng T? Do",
                    Description = "B?n ?� ki�n tr� ???c 1 th�ng kh�ng h�t thu?c!",
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/1035/1035688.png",
                    BadgeColor = "#17a2b8"
                }
            },
            new UserAchievement
            {
                AchievedDate = DateTime.Today.AddDays(-1),
                Achievement = new Achievement
                {
                    Title = "Ng�y Kh�ng Kh�i",
                    Description = "Th�m m?t ng�y s?ch s?, b?n ?ang l�m r?t t?t!",
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/190/190411.png",
                    BadgeColor = "#ffc107"
                }
            }
        };
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
