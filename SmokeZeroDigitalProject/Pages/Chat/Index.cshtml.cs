using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokeZeroDigitalSolution.Application.Features.Chat.DTOs;

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
        try
        {
            //Console.WriteLine("======= DEBUG START =======");
            //Console.WriteLine($"BaseAddress: {_http.BaseAddress}");

            var payload = new
            {
                AppUserId = CurrentUserId,
                CoachId = CoachId
            };

            //Console.WriteLine($"Calling POST /api/Chat/conversation with payload: {System.Text.Json.JsonSerializer.Serialize(payload)}");

            var response = await _http.PostAsJsonAsync("/api/Chat/conversation", payload);
            //Console.WriteLine($"Conversation response status: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error content: " + error);
                return;
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiSuccessResult<ConversationDto>>();
            if (apiResponse?.Content == null || apiResponse.Content.Id == Guid.Empty)
            {
                Console.WriteLine("? Conversation deserialize failed or ID is Guid.Empty");
                return;
            }

            ConversationId = apiResponse.Content.Id;
            //Console.WriteLine("✅ Conversation ID: " + ConversationId);

            var msgUrl = $"/api/Chat/messages/{ConversationId}";
            //Console.WriteLine("Calling GET " + msgUrl);

            var msgResponse = await _http.GetAsync(msgUrl);
            if (!msgResponse.IsSuccessStatusCode)
            {
                var error = await msgResponse.Content.ReadAsStringAsync();
                Console.WriteLine("? Error khi lấy messages: " + error);
                return;
            }

            var msgApi = await msgResponse.Content.ReadFromJsonAsync<ApiSuccessResult<List<ChatMessageDto>>>();
            Messages = msgApi?.Content ?? new();
            //Console.WriteLine($"✅ Message count: {Messages.Count}");
            //Console.WriteLine("======= DEBUG END =======");
        }
        catch (Exception ex)
        {
            Console.WriteLine("🔥 Exception occurred:");
            Console.WriteLine(ex.ToString());
            throw;
        }
    }


}
