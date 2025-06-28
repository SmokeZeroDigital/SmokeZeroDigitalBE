namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class BlogArticle : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "text")]
        public string Content { get; set; } = string.Empty;
        public Guid? AuthorUserId { get; set; } // Người viết (có thể là Admin/Coach)
        public AppUser? AuthorUser { get; set; } // Navigation Property
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;
        public string? Tags { get; set; } // Dùng CSV hoặc JSON array
        public int ViewCount { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
