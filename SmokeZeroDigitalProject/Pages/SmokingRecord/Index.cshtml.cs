using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs;

namespace SmokeZeroDigitalProject.Pages.SmokingRecord
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApiConfig _apiConfig;

        public List<SmokingRecordDto>? SmokingRecords { get; set; }

        public IndexModel(IHttpClientFactory clientFactory, ApiConfig apiConfig)
        {
            _clientFactory = clientFactory;
            _apiConfig = apiConfig;
        }

        public async Task<IActionResult> OnGetAsync(Guid userId)
        {
            var http = _clientFactory.CreateClient("ApiClient");
            var url = _apiConfig.GetEndpoint(ApiEndpoints.GetSmokingRecordsByUser.Replace("{userId}", userId.ToString()));
            var apiResult = await http.GetFromJsonAsync<ApiSuccessResult<List<SmokingRecordDto>>>(url);
            SmokingRecords = apiResult?.Content;

            return Page();
        }
    }
}
