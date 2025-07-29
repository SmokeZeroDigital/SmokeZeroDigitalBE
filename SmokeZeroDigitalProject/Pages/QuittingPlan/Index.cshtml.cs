using SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.DTOs;

namespace SmokeZeroDigitalProject.Pages.QuittingPlan
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApiConfig _apiConfig;

        public QuittingPlanDto? Plan { get; set; }

        public IndexModel(IHttpClientFactory clientFactory, ApiConfig apiConfig)
        {
            _clientFactory = clientFactory;
            _apiConfig = apiConfig;
        }

        public async Task OnGetAsync(Guid userId)
        {
            var http = _clientFactory.CreateClient("ApiClient");
            var url = _apiConfig.GetEndpoint(ApiEndpoints.GetQuittingPlanByUser.Replace("{userId}", userId.ToString()));
            var apiResult = await http.GetFromJsonAsync<ApiSuccessResult<QuittingPlanDto>>(url);
            Plan = apiResult?.Content;
        }
    }
}