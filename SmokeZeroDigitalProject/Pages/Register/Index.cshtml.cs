namespace SmokeZeroDigitalProject.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly ApiConfig _apiConfig;

        public IndexModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }

        [BindProperty]
        public RegisterRequest RegisterRequest { get; set; } = new();
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
                TempData["ToastMessage"] = "error:Vui lòng điền đầy đủ các trường bắt buộc.";
                return Page();
            }

            if (RegisterRequest.Password != RegisterRequest.ConfirmPassword)
            {
                TempData["ToastMessage"] = "error:Mật khẩu không khớp.";
                return Page();
            }

            var registerUrl = _apiConfig.GetEndpoint(ApiEndpoints.Register);

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(registerUrl, RegisterRequest);

            if (response.IsSuccessStatusCode)
            {
                TempData["ToastMessage"] = "success:Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToPage("/Login/Index");
            }
            else
            {
                TempData["ToastMessage"] = "error:Đăng ký thất bại. Email hoặc Username có thể đã tồn tại.";
                return Page();
            }
        }
    }
}