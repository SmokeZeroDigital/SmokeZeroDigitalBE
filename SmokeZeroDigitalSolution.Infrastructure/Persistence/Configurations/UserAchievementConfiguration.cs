namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
{
    public class UserAchievementConfiguration : IEntityTypeConfiguration<UserAchievement>
    {
        public void Configure(EntityTypeBuilder<UserAchievement> builder)
        {
            builder.HasKey(ua => ua.Id);
            builder.Property(ua => ua.Id).ValueGeneratedOnAdd();

            builder.Property(ua => ua.DateAchieved)
                .IsRequired();

            // Audit properties from BaseEntity
            builder.Property(ua => ua.CreatedAt)
                   .IsRequired();
            builder.Property(ua => ua.LastModifiedAt)
                   .IsRequired(false);

            builder.HasOne(ua => ua.User)
                .WithMany(u => u.UserAchievements)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ua => ua.Achievement)
                .WithMany(a => a.UserAchievements)
                .HasForeignKey(ua => ua.AchievementId)
                .OnDelete(DeleteBehavior.Cascade);

            // Nếu bạn muốn cấu hình thêm ràng buộc độc nhất (UserId, AchievementId)
            builder.HasIndex(ua => new { ua.UserId, ua.AchievementId }).IsUnique();
        }
    }
}