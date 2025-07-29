namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(c => c.CommentDate)
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .IsRequired();

            // Audit properties from BaseEntity
            builder.Property(c => c.CreatedAt)
                   .IsRequired();
            builder.Property(c => c.LastModifiedAt)
                   .IsRequired(false);

            // Mối quan hệ với AppUser đã được cấu hình trong AppUserConfiguration

            // Mối quan hệ với Post (có thể null)
            builder.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .IsRequired(false) // Nullable
                .OnDelete(DeleteBehavior.Cascade); // Khi Post bị xóa, Comments liên quan cũng bị xóa

            // Mối quan hệ với BlogArticle (có thể null)
            builder.HasOne(c => c.Article)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.ArticleId)
                .IsRequired(false) // Nullable
                .OnDelete(DeleteBehavior.Cascade); // Khi BlogArticle bị xóa, Comments liên quan cũng bị xóa

            // Mối quan hệ với AppUser
            builder.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Mối quan hệ tự tham chiếu (ParentComment)
            builder.HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .IsRequired(false) // Nullable (là comment gốc hoặc trả lời)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xóa cha khi con tồn tại

            // Kiểm tra ràng buộc để đảm bảo chỉ PostId HOẶC ArticleId có giá trị (không cả hai, không cả không)
            // Lệnh này có thể gây lỗi nếu DB không hỗ trợ hoặc nếu bạn dùng Initial Migration với DB đã tồn tại.
            // Nên cân nhắc xử lý logic này ở tầng Application Service để linh hoạt hơn.
        }
    }
}