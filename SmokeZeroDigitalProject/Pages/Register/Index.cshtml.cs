namespace SmokeZeroDigitalProject.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public RegisterRequest RegisterRequest { get; set; } = new();

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

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
                ErrorMessage = "Please fill in all required fields.";
                return Page();
            }

            if (RegisterRequest.Password != RegisterRequest.ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }

            var baseUrl = _configuration["ApiSettings:BaseUrl"];
            var registerUrl = $"{baseUrl}/api/Auth/register";

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(registerUrl, RegisterRequest);

            if (response.IsSuccessStatusCode)
            {
                SuccessMessage = "Registration successful! Please login.";
                return RedirectToPage("/Login/Index");
            }
            else
            {
                ErrorMessage = "Registration failed. Please check your information.";
                return Page();
            }
        }
    }
}
