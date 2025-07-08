namespace SmokeZeroDigitalProject.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly ApiConfig _apiConfig;

        public LoginModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }

        [BindProperty]
        public LoginRequest LoginRequest { get; set; } = new();

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(LoginRequest.UserName) || string.IsNullOrWhiteSpace(LoginRequest.Password))
            {
                ErrorMessage = "Username and password are required.";
                return Page();
            }

            var loginUrl = _apiConfig.GetEndpoint(ApiEndpoints.Login);

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(loginUrl, LoginRequest);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }
        }
    }
}
