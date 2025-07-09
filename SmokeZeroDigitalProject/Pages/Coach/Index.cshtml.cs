namespace SmokeZeroDigitalProject.Pages.Coach
{
    public class IndexModel : PageModel
    {
        private readonly ApiConfig _apiConfig;

        public IndexModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }

        public List<CoachResponseDto> Coaches { get; set; } = new();
        public int Total { get; set; }

        public async Task OnGetAsync()
        {
            var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetAllCoach);

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var apiResult = JsonSerializer.Deserialize<ApiSuccessResult<CoachListResponseDto>>(responseBody, options);
                Coaches = apiResult?.Content?.Data ?? new List<CoachResponseDto>();
                Total = apiResult?.Content?.Total ?? 0;
            }

        }
    }
}
