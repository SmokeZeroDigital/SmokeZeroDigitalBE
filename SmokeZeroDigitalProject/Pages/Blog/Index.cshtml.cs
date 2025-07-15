using SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs;
using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalProject.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly ApiConfig _apiConfig;

        public IndexModel(IConfiguration configuration)
        {
            _apiConfig = new ApiConfig(configuration);
        }
        public List<BlogReponseDto> Blogs { get; set; } = new();
        public string? SearchKeyword { get; set; }
        public string? SelectedTag { get; set; }
        public string? SortBy { get; set; }
        public async Task<IActionResult> OnGetAsync(string? searchKeyword, string? selectedTag, string? sortBy)
        {
            SearchKeyword = searchKeyword;
            SelectedTag = selectedTag;
            SortBy = sortBy;

            var sessionKey = "AllBlogs";
            string? blogsJson = HttpContext.Session.GetString(sessionKey);

            if (string.IsNullOrEmpty(blogsJson))
            {
                var apiUrl = _apiConfig.GetEndpoint(ApiEndpoints.GetAllBlogs);
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    Blogs = new();
                    return Page();
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var apiResult = JsonSerializer.Deserialize<ApiSuccessResult<List<BlogReponseDto>>>(responseBody, options);
                Blogs = apiResult?.Content ?? new();

                HttpContext.Session.SetString(sessionKey, JsonSerializer.Serialize(Blogs));
            }
            else
            {
                Blogs = JsonSerializer.Deserialize<List<BlogReponseDto>>(blogsJson) ?? new();
            }

            if (!string.IsNullOrWhiteSpace(SearchKeyword))
            {
                var keyword = SearchKeyword.Trim().ToLower();
                Blogs = Blogs.Where(b =>
                    (!string.IsNullOrEmpty(b.Title) && b.Title.ToLower().Contains(keyword)) ||
                    (!string.IsNullOrEmpty(b.AuthorName) && b.AuthorName.ToLower().Contains(keyword)) ||
                    (b.Tags != null && b.Tags.ToLower().Contains(keyword))
                ).ToList();
            }
            if (!string.IsNullOrWhiteSpace(SelectedTag))
            {
                var keyword = SelectedTag.Trim().ToLower();
                Blogs = Blogs.Where(b =>
                    !string.IsNullOrEmpty(b.Tags) && b.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Any(t => t.Trim().ToLower() == keyword)
                ).ToList();
            }
            Blogs = SortBy switch
            {
                "views" => Blogs.OrderByDescending(b => b.ViewCount).ToList(),
                "title" => Blogs.OrderBy(b => b.Title).ToList(),
                _ => Blogs.OrderByDescending(b => b.CreatedAt).ToList(), 
            };
            return Page();
        }
    }
}
