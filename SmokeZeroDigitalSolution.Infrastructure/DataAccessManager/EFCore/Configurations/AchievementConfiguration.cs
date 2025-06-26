namespace SmokeZeroDigitalSolution.Infrastructure.DataAccessManager.EFCore.Configurations
{
    public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.HasKey(a => a.Id); // Id từ BaseEntity hoặc tự định nghĩa
            builder.Property(a => a.Id).ValueGeneratedOnAdd(); // Đảm bảo Id là tự tăng (nếu là Guid, không phải tự tăng mà là được tạo khi Add)

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Description)
                .IsRequired() // Description có thể có dữ liệu bắt buộc
                .HasMaxLength(500);

            builder.Property(a => a.IconUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(a => a.ThresholdValue)
                .IsRequired();

            // Giả sử ThresholdType là một chuỗi mô tả, nếu là Enum thì dùng HasConversion<string>()
            builder.Property(a => a.ThresholdType)
                .IsRequired()
                .HasMaxLength(50);

            // Audit properties from BaseEntity
            builder.Property(a => a.CreatedAt)
                   .IsRequired();
            builder.Property(a => a.LastModifiedAt)
                   .IsRequired(false);

            // Mối quan hệ 1-N với UserAchievement
            builder.HasMany(a => a.UserAchievements)
                .WithOne(ua => ua.Achievement)
                .HasForeignKey(ua => ua.AchievementId)
                .OnDelete(DeleteBehavior.Cascade); // Khi một Achievement bị xóa, các UserAchievement liên quan cũng bị xóa
        }
    }
}
