using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs;
using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalProject.Pages.Blog
{
    public class DetailModel : PageModel
    {
        private readonly ApiConfig _apiConfig;

        public DetailModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }  

        public BlogArticle? Blog { get; set; }
        public List<CommentDto> Comments { get; set; }
        public string? CurrentUserId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            HttpContext.Session.SetString("UserId", "f3f63f9a-41e4-4445-9e00-11d11f86acc3");
            CurrentUserId = HttpContext.Session.GetString("UserId");

            HttpContext.Session.Remove("AllBlogs");
            var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetBlogById).Replace("{id}", Id.ToString());

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };


            var apiResult = JsonSerializer.Deserialize<ApiSuccessResult<BlogArticle>>(responseBody, options);
            Blog = apiResult?.Content;

            if (Blog == null)
            {
                return NotFound();
            }
            var commentApiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetCommentByArticleId)
                              .Replace("{id}", Id.ToString());

            var commentResponse = await httpClient.GetAsync(commentApiUrl);
            Comments = new List<CommentDto>();

            if (commentResponse.IsSuccessStatusCode)
            {
                var commentBody = await commentResponse.Content.ReadAsStringAsync();
                var commentResult = JsonSerializer.Deserialize<ApiSuccessResult<List<CommentDto>>>(commentBody, options);

                if (commentResult?.Content != null)
                {
                    Comments = commentResult.Content;
                }
            }


            return Page();
        }
    }
}
