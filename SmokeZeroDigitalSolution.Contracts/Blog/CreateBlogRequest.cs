namespace SmokeZeroDigitalSolution.Contracts.Blog
{
    public class CreateBlogRequest
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public Guid AuthorUserId { get; set; }

        [StringLength(500)]
        public string? Tags { get; set; }
    }
}
