namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Content)
                .HasColumnType("text") // Hỗ trợ nội dung lớn (text/image URLs)
                .IsRequired();

            builder.Property(p => p.PostDate)
                .IsRequired();

            builder.Property(p => p.LikesCount)
                .IsRequired();

            // Audit properties from BaseEntity
            builder.Property(p => p.CreatedAt)
                   .IsRequired();
            builder.Property(p => p.LastModifiedAt)
                   .IsRequired(false);

            // Mối quan hệ với AppUser đã được cấu hình trong AppUserConfiguration

            // Mối quan hệ 1-N với Comment
            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .IsRequired(false) // Comment có thể thuộc về Article
                .OnDelete(DeleteBehavior.Cascade); // Khi Post bị xóa, Comments liên quan cũng bị xóa

            builder.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}