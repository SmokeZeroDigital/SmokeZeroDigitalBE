namespace SmokeZeroDigitalSolution.Infrastructure.DataAccessManager.EFCore.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).ValueGeneratedOnAdd();

            builder.Property(n => n.Type)
                .IsRequired()
                .HasMaxLength(50); // "Motivation", "Reminder", "Achievement"

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(n => n.Message)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(n => n.ScheduledTime)
                .IsRequired();

            builder.Property(n => n.SentTime)
                .IsRequired(false);

            builder.Property(n => n.IsRead)
                .IsRequired();

            // Audit properties from BaseEntity
            builder.Property(n => n.CreatedAt)
                   .IsRequired();
            builder.Property(n => n.LastModifiedAt)
                   .IsRequired(false);

            // Mối quan hệ với AppUser đã được cấu hình trong AppUserConfiguration
        }
    }
}