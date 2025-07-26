using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmokeZeroDigitalSolution.Application.Features.NotificationManager.DTOs;
using SmokeZeroDigitalSolution.Domain.Entites;
using System.Text;

namespace SmokeZeroDigitalProject.Pages.Notification
{
    public class CreateModel : PageModel
    {
        private readonly ApiConfig _apiConfig;

        public CreateModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }

        [BindProperty]
        public CreateNotiDto Noti { get; set; } = new();

        public List<AppUser> Users { get; set; } = new();

        public SelectList UserSelectList => new(Users, "Id", "FullName");

        public async Task<IActionResult> OnGetAsync()
        {
            var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetAllUser);

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Response is successful.");
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var apiResult = JsonSerializer.Deserialize<ApiSuccessResult<List<AppUser>>>(responseBody, options);
                
                var appUsers = apiResult?.Content ?? new List<AppUser>();

                Users = appUsers
                    .Where(user => user.CurrentSubscriptionPlanId != null) 
                    .ToList();

              
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return Page();
            }
            //HttpContext.Session.SetString("UserId", "f3f63f9a-41e4-4445-9e00-11d11f86acc3");
            //Feedback.UserId = Guid.Parse(HttpContext.Session.GetString("UserId") ?? string.Empty);
            var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.CreateNoti);
            using var httpClient = new HttpClient();

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(Noti),
                Encoding.UTF8,
                "application/json"
            );

            var response = await httpClient.PostAsync(apiUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error.");
                return Page();
            }

            TempData["SuccessMessage"] = "Success";
            return RedirectToPage();
        }
    }
}
