using System.ComponentModel.DataAnnotations;

namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs
{
    public class UpdateCommentDto
    {
        public Guid Id { get; set; }  // Comment ID to update
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; } = string.Empty; 
    }
}
