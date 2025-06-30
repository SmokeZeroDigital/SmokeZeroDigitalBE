namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
{
    public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(a => a.IconUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(a => a.ThresholdValue)
                .IsRequired();

            builder.Property(a => a.ThresholdType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.CreatedAt)
                   .IsRequired();
            builder.Property(a => a.LastModifiedAt)
                   .IsRequired(false);

            builder.HasMany(a => a.UserAchievements)
                .WithOne(ua => ua.Achievement)
                .HasForeignKey(ua => ua.AchievementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
