namespace SmokeZeroDigitalSolution.Infrastructure.DataAccessManager.EFCore.Configurations
{
    public class BlogArticleConfiguration : IEntityTypeConfiguration<BlogArticle>
    {
        public void Configure(EntityTypeBuilder<BlogArticle> builder)
        {
            builder.HasKey(ba => ba.Id);
            builder.Property(ba => ba.Id).ValueGeneratedOnAdd();

            builder.Property(ba => ba.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(ba => ba.Content)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(ba => ba.PublishDate)
                .IsRequired();

            builder.Property(ba => ba.Tags)
                .HasMaxLength(500) // Dùng CSV hoặc JSON array string
                .IsRequired(false);

            builder.Property(ba => ba.ViewCount)
                .IsRequired();

            // Audit properties from BaseEntity
            builder.Property(ba => ba.CreatedAt)
                   .IsRequired();
            builder.Property(ba => ba.LastModifiedAt)
                   .IsRequired(false);

            // Mối quan hệ 1-N với Comment
            builder.HasMany(ba => ba.Comments)
                .WithOne(c => c.Article)
                .HasForeignKey(c => c.ArticleId)
                .IsRequired(false) // Comment có thể thuộc về Post hoặc Article
                .OnDelete(DeleteBehavior.Cascade); // Khi BlogArticle bị xóa, Comments liên quan cũng bị xóa

            // Mối quan hệ với AuthorUser đã được cấu hình trong AppUserConfiguration
        }
    }
}