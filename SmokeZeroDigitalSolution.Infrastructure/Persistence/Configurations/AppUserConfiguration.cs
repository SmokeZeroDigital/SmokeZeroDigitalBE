namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Các thuộc tính cơ bản từ IdentityUser đã được IdentityDbContext xử lý

            // Các thuộc tính riêng của AppUser
            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.DateOfBirth)
                .IsRequired(false);

            builder.Property(u => u.Gender)
                .IsRequired(); // Giả sử Gender là bắt buộc

            builder.Property(u => u.ProfilePictureUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(u => u.RegistrationDate)
                .IsRequired(false);

            builder.Property(u => u.CurrentMoneySaved)
                .HasColumnType("decimal(18,2)") // Đảm bảo kiểu dữ liệu chính xác cho tiền tệ
                .IsRequired(false);

            builder.Property(u => u.DaysSmokingFree)
                .IsRequired(false);

            builder.Property(u => u.HealthImprovements)
                .HasMaxLength(1000)
                .IsRequired(false);

            // Audit properties (tự thêm vào AppUser, không phải từ BaseEntity kế thừa)
            builder.Property(u => u.CreatedAt)
                   .IsRequired(false);
            builder.Property(u => u.LastModifiedAt)
                   .IsRequired(false);
            builder.Property(u => u.IsDeleted)
                   .HasDefaultValue(false)
                   .IsRequired(false);

            // Mối quan hệ 1-0..1 với SubscriptionPlan
            builder.HasOne(u => u.CurrentSubscriptionPlan)
                .WithMany() // SubscriptionPlan không có collection của User
                .HasForeignKey(u => u.CurrentSubscriptionPlanId)
                .IsRequired(false) // Có thể không có gói đăng ký
                .OnDelete(DeleteBehavior.SetNull); // Khi SubscriptionPlan bị xóa, CurrentSubscriptionPlanId của User được đặt null

            // Mối quan hệ 1-N với QuittingPlan
            builder.HasMany(u => u.QuittingPlans)
                .WithOne(qp => qp.User)
                .HasForeignKey(qp => qp.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Khi User bị xóa, các QuittingPlan liên quan cũng bị xóa

            // Mối quan hệ 1-N với SmokingRecord
            builder.HasMany(u => u.SmokingRecords)
                .WithOne(sr => sr.User)
                .HasForeignKey(sr => sr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ 1-N với ProgressEntry
            builder.HasMany(u => u.ProgressEntries)
                .WithOne(pe => pe.User)
                .HasForeignKey(pe => pe.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ 1-N với UserAchievement
            builder.HasMany(u => u.UserAchievements)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ 1-N với Post
            builder.HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ 1-N với Comment
            builder.HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ 1-N với Feedback
            builder.HasMany(u => u.Feedbacks)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ 1-N với BlogArticle (AuthoredArticles)
            builder.HasMany(u => u.AuthoredArticles)
                .WithOne(ba => ba.AuthorUser)
                .HasForeignKey(ba => ba.AuthorUserId)
                .IsRequired(false) // Một bài viết có thể không có tác giả là AppUser (VD: Guest post)
                .OnDelete(DeleteBehavior.SetNull); // Khi AppUser tác giả bị xóa, AuthorUserId được đặt null

            // Mối quan hệ 1-N với Coach (một AppUser có thể là một Coach)
            builder.HasOne(u => u.Coach)
                .WithOne(c => c.User)
                .HasForeignKey<Coach>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Khi AppUser bị xóa, Coach liên kết cũng bị xóa

            // Mối quan hệ 1-N với ChatMessage (SentMessages)
            builder.HasMany(u => u.SentMessages)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}