﻿using SmokeZeroDigitalSolution.Domain.Entites;

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
    public Guid CurrentUserId => Guid.Parse(HttpContext.Session.GetString("UserId") ?? Guid.Empty.ToString());
    public string Role => HttpContext.Session.GetString("UserRole") ?? string.Empty;
    public List<ConversationDto> UserConversations { get; set; } = new();
    public bool CanChat { get; set; }
    public bool IsCoach { get; set; } = true;



    public async Task OnGetAsync(Guid? conversationId = null)
    {
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
                return;
            }

            if (!CanChat) return;
        }

        var convResponse = await _http.GetAsync($"/api/Chat/conversationByUserId/{CurrentUserId}");
        Console.WriteLine(convResponse);
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