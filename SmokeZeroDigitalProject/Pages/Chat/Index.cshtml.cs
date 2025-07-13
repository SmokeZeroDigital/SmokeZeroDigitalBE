namespace SmokeZeroDigitalProject.Pages.Chat;

public class ChatPageModel : PageModel
{
    private readonly HttpClient _http;

    public ChatPageModel(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory.CreateClient("ChatClient");
    }

    public List<ChatMessageDto> Messages { get; set; } = new();
    public Guid ConversationId { get; set; }
    public Guid CurrentUserId => Guid.Parse("00000000-0000-0000-0000-000000000004");
    public Guid CoachId => Guid.Parse("10000000-0000-0000-0000-000000000001");

    public async Task OnGetAsync()
    {
        var payload = new { AppUserId = CurrentUserId, CoachId = CoachId };
        var convResponse = await _http.PostAsJsonAsync("/api/Chat/conversation", payload);
        if (!convResponse.IsSuccessStatusCode) return;

        var convResult = await convResponse.Content.ReadFromJsonAsync<ApiSuccessResult<ConversationDto>>();
        if (convResult?.Content?.Id == Guid.Empty) return;

        ConversationId = convResult.Content.Id;

        var msgResponse = await _http.GetAsync($"/api/Chat/messages/{ConversationId}");
        if (!msgResponse.IsSuccessStatusCode) return;

        var msgResult = await msgResponse.Content.ReadFromJsonAsync<ApiSuccessResult<List<ChatMessageDto>>>();
        Messages = msgResult?.Content ?? new();
    }
}
