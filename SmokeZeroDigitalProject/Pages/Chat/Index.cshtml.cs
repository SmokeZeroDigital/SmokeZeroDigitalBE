using SmokeZeroDigitalProject.Common.Helper;
using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalProject.Pages.Chat;

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
    public Guid CurrentUserId { get; set; } = Guid.Empty;
    public string Role { get; set; } = string.Empty;
    public List<ConversationDto> UserConversations { get; set; } = new();
    public bool CanChat { get; set; }
    public bool IsCoach { get; set; } = false;



    public async Task<IActionResult> OnGetAsync(Guid? conversationId = null)
    {
        var token = HttpContext.Session.GetString("Token");
        if (string.IsNullOrEmpty(token))
        {
            TempData["ToastMessage"] = "error:Bạn chưa đăng nhập.";
            return RedirectToPage("/Login");
        }


        var userIdStr = JwtTokenHelper.GetClaim(token, "UserId");

        Role = JwtTokenHelper.GetClaim(token, "role") ?? string.Empty;

        IsCoach = Role == "Coach";

        if (!Guid.TryParse(userIdStr, out Guid parsedUserId))
        {
            TempData["ToastMessage"] = "error:Token không hợp lệ.";
            return Redirect("/login");
        }

        CurrentUserId = parsedUserId;

        if (!IsCoach)
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
                return Page();
            }

            if (!CanChat) return Page();
        }

        var convResponse = await _http.GetAsync($"/api/Chat/conversationByUserId/{CurrentUserId}");
        if (convResponse.IsSuccessStatusCode)
        {
            var convResult = await convResponse.Content.ReadFromJsonAsync<ApiSuccessResult<List<ConversationDto>>>();
            UserConversations = convResult?.Content ?? new();
        }

        if (conversationId.HasValue)
        {
            ConversationId = conversationId.Value;
            await LoadMessages(ConversationId);
        }
        else if (UserConversations.Any())
        {
            ConversationId = UserConversations.First().Id;
            await LoadMessages(ConversationId);
        }

        return Page();
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