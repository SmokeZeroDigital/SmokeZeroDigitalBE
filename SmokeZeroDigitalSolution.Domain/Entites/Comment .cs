namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class Comment : BaseEntity
    {
        public Guid UserId { get; set; } // Foreign Key
        public AppUser User { get; set; } = default!; // Navigation Property

        public Guid? PostId { get; set; } // Foreign Key (có thể null nếu là comment cho Article)
        public Post? Post { get; set; } // Navigation Property

        public Guid? ArticleId { get; set; } // Foreign Key (có thể null nếu là comment cho Post)
        public BlogArticle? Article { get; set; } // Navigation Property

        public string Content { get; set; } = string.Empty;
        public DateTime CommentDate { get; set; } = DateTime.UtcNow;

        public Guid? ParentCommentId { get; set; } // Foreign Key cho bình luận trả lời
        public Comment? ParentComment { get; set; } // Navigation Property

        public ICollection<Comment> Replies { get; set; } = new List<Comment>(); // Navigation Property cho các bình luận con
        public bool IsDeleted { get; set; } = false;
    }
}
