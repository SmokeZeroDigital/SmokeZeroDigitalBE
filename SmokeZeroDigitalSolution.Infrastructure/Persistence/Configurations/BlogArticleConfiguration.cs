namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
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
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(ba => ba.ViewCount)
                .IsRequired();

            builder.Property(ba => ba.CreatedAt)
                   .IsRequired();
            builder.Property(ba => ba.LastModifiedAt)
                   .IsRequired(false);

            builder.HasMany(ba => ba.Comments)
                .WithOne(c => c.Article)
                .HasForeignKey(c => c.ArticleId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ba => ba.AuthorUser)
                .WithMany()
                .HasForeignKey(ba => ba.AuthorUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}