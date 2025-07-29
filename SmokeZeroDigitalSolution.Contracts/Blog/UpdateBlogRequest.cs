namespace SmokeZeroDigitalSolution.Contracts.Blog
{
    public class UpdateBlogRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Tags { get; set; }
    }
}
