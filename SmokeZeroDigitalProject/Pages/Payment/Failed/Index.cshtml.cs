using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmokeZeroDigitalProject.Pages.Payment.Failed
{
    public class IndexModel : PageModel
    {
        public string Status { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;

        public void OnGet(string status, string amount)
        {
            Status = status ?? string.Empty;
            Amount = amount ?? string.Empty;
        }
    }
}
