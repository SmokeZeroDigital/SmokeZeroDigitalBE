namespace SmokeZeroDigitalSolution.Infrastructure.DataAccessManager.EFCore.Configurations
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

            // Mối quan hệ với AppUser đã được cấu hình trong AppUserConfiguration
            // Mối quan hệ với Achievement đã được cấu hình trong AchievementConfiguration

            // Nếu bạn muốn cấu hình thêm ràng buộc độc nhất (UserId, AchievementId)
            builder.HasIndex(ua => new { ua.UserId, ua.AchievementId }).IsUnique();
        }
    }
}