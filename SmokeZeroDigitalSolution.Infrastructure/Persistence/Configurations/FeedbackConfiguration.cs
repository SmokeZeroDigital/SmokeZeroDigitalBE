namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(f => f.TargetEntityId)
                .IsRequired();

            builder.Property(f => f.TargetEntityType)
                .IsRequired()
                .HasMaxLength(50); // "COACH", "PLATFORM", v.v.

            builder.Property(f => f.Rating)
                .IsRequired();

            builder.Property(f => f.Comment)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(f => f.FeedbackDate)
                .IsRequired();

            // Audit properties from BaseEntity
            builder.Property(f => f.CreatedAt)
                   .IsRequired();
            builder.Property(f => f.LastModifiedAt)
                   .IsRequired(false);

            // Mối quan hệ với AppUser đã được cấu hình trong AppUserConfiguration

            // Mối quan hệ với Coach (FeedbacksGiven) đã được xử lý trong CoachConfiguration
            // Nó là mối quan hệ một chiều từ Coach đến Feedback dựa trên TargetEntityId và TargetEntityType.
            // Nếu bạn muốn navigation property ngược lại từ Feedback đến Coach, cần thêm CoachId vào Feedback.
            // Hiện tại, với TargetEntityId và TargetEntityType, bạn sẽ phải join hoặc lọc thủ công.
        }
    }
}