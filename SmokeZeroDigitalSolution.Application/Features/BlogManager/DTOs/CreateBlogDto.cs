using System.ComponentModel.DataAnnotations;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs
{
    public class CreateBlogDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public Guid AuthorUserId { get; set; }

        [StringLength(500)]
        public string? Tags { get; set; } // Format: "tag1,tag2,tag3"
    }
}
