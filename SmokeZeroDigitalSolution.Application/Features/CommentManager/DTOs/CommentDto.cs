namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;  // From User entity

        public Guid? PostId { get; set; }
        public Guid? ArticleId { get; set; }
        public Guid? ParentCommentId { get; set; }

        public string Content { get; set; } = string.Empty;
        public DateTime CommentDate { get; set; }
        public bool IsDeleted { get; set; }

        public List<CommentDto> Replies { get; set; } = new();  // Nested replies
    }
}
