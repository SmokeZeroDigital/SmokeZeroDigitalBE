using System.ComponentModel.DataAnnotations;

namespace SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs
{
    public class CreateFeedbackDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid CoachId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Nội dung phản hồi không vượt quá 500 ký tự.")]
        public string Content { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "Đánh giá từ 1 đến 5 sao.")]
        public int Rating { get; set; }
    }

}
