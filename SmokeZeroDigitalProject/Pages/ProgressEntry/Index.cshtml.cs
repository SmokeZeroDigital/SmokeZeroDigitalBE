using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs;

namespace SmokeZeroDigitalProject.Pages.ProgressEntry
{
    public class IndexModel : PageModel
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly ApiConfig _apiConfig;

        public List<ProgressEntryDto>? ProgressEntries { get; set; }

        public IndexModel(IHttpClientFactory clientFactory, ApiConfig apiConfig)
        {
            _clientFactory = clientFactory;
            _apiConfig = apiConfig;
        }

        public async Task<IActionResult> OnGetAsync(Guid userId)
        {
            var http = _clientFactory.CreateClient("ApiClient");
            var url = _apiConfig.GetEndpoint(ApiEndpoints.GetProgressEntriesByUser.Replace("{userId}", userId.ToString()));
            var apiResult = await http.GetFromJsonAsync<ApiSuccessResult<List<ProgressEntryDto>>>(url);
            ProgressEntries = apiResult?.Content;

            return Page();
        }

    }
}