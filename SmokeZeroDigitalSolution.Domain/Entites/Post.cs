namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class Post : BaseEntity
    {
        public Guid UserId { get; set; } // Foreign Key
        public AppUser User { get; set; } = default!; // Navigation Property

        public string Content { get; set; } = string.Empty; // Supports text/image URLs
        public DateTime PostDate { get; set; } = DateTime.UtcNow;
        public int LikesCount { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
