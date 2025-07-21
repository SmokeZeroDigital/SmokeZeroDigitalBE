using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalProject.Pages.Chat;

//public class ChatPageModel : PageModel
//{
//    private readonly HttpClient _http;

//    public ChatPageModel(IHttpClientFactory httpClientFactory)
//    {
//        _http = httpClientFactory.CreateClient("ChatClient");
//    }

//    public List<ChatMessageDto> Messages { get; set; } = new();
//    public Guid ConversationId { get; set; }
//    public Guid CurrentUserId => Guid.Parse("00000000-0000-0000-0000-000000000004");
//    public Guid CoachId => Guid.Parse("10000000-0000-0000-0000-000000000001");

//    public async Task OnGetAsync()
//    {
//        var payload = new { AppUserId = CurrentUserId, CoachId = CoachId };
//        var convResponse = await _http.PostAsJsonAsync("/api/Chat/conversation", payload);
//        if (!convResponse.IsSuccessStatusCode) return;

//        var convResult = await convResponse.Content.ReadFromJsonAsync<ApiSuccessResult<ConversationDto>>();
//        if (convResult?.Content?.Id == Guid.Empty) return;

//        ConversationId = convResult.Content.Id;

//        var msgResponse = await _http.GetAsync($"/api/Chat/messages/{ConversationId}");
//        if (!msgResponse.IsSuccessStatusCode) return;

//        var msgResult = await msgResponse.Content.ReadFromJsonAsync<ApiSuccessResult<List<ChatMessageDto>>>();
//        Messages = msgResult?.Content ?? new();
//    }
//}
public class ChatPageModel : PageModel
{
    private readonly HttpClient _http;
    private readonly IHttpContextAccessor _contextAccessor;

    public ChatPageModel(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor)
    {
        _http = httpClientFactory.CreateClient("ChatClient");
        _contextAccessor = contextAccessor;
    }

    public List<ChatMessageDto> Messages { get; set; } = new();
    public Guid ConversationId { get; set; }
    public Guid CurrentUserId => Guid.Parse(HttpContext.Session.GetString("UserId") ?? Guid.Empty.ToString());
    public string Role => HttpContext.Session.GetString("UserRole") ?? string.Empty;
    public List<ConversationDto> UserConversations { get; set; } = new();
    public bool CanChat { get; set; }

    public async Task OnGetAsync(Guid? conversationId = null)
    {
        var userResponse = await _http.GetAsync($"/api/User/{CurrentUserId}");
        if (userResponse.IsSuccessStatusCode)
        {
            var userResult = await userResponse.Content.ReadFromJsonAsync<ApiSuccessResult<AppUser>>();
            CanChat = userResult?.Content?.CurrentSubscriptionPlanId != null;
        }
        else
        {
            CanChat = false;
            return;
        }

        if (!CanChat) return;

        if (Role == "COACH")
        {
            var response = await _http.GetAsync($"/api/Chat/conversations?coachId={CurrentUserId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiSuccessResult<List<ConversationDto>>>();
                UserConversations = result?.Content ?? new();
            }

            if (conversationId.HasValue)
            {
                ConversationId = conversationId.Value;
                await LoadMessages(ConversationId);
            }
        }
        else
        {
            var payload = new { AppUserId = CurrentUserId };
            var convResponse = await _http.PostAsJsonAsync("/api/Chat/conversation", payload);
            if (!convResponse.IsSuccessStatusCode) return;

            var convResult = await convResponse.Content.ReadFromJsonAsync<ApiSuccessResult<ConversationDto>>();
            if (convResult?.Content?.Id == Guid.Empty) return;

            ConversationId = convResult.Content.Id;
            await LoadMessages(ConversationId);
        }
    }

    private async Task LoadMessages(Guid conversationId)
    {
        var msgResponse = await _http.GetAsync($"/api/Chat/messages/{conversationId}");
        if (msgResponse.IsSuccessStatusCode)
        {
            var msgResult = await msgResponse.Content.ReadFromJsonAsync<ApiSuccessResult<List<ChatMessageDto>>>();
            Messages = msgResult?.Content ?? new();
        }
    }
}
