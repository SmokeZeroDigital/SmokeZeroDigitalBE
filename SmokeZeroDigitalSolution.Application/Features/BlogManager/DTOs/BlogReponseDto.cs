namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs
{
    public class BlogReponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? AuthorName { get; set; } 
        public DateTime PublishDate { get; set; }
        public string[]? Tags { get; set; } 
        public int ViewCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
