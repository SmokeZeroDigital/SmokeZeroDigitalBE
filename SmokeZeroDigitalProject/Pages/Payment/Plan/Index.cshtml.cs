namespace SmokeZeroDigitalProject.Pages.Plan
{
    public class SubcriptionPlanModel : PageModel
    {
        private readonly ApiConfig _apiConfig;

        public SubcriptionPlanModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }

        public List<GetPlanResponseDto> Plans { get; set; } = new();

        public async Task OnGetAsync()
        {
            var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetAllPlan);

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var apiResult = JsonSerializer.Deserialize<ApiSuccessResult<List<GetPlanResponseDto>>>(responseBody, options);
                Plans = apiResult?.Content ?? new List<GetPlanResponseDto>();
            }
        }

        public IActionResult OnPost(Guid planId)
        {
            // Lưu planId vào session
            HttpContext.Session.SetString("SelectedPlanId", planId.ToString());

            // Redirect đến trang chọn coach
            return RedirectToPage("/Payment/Coach/Index");
        }
    }
}