namespace SmokeZeroDigitalSolution.Infrastructure.DataAccessManager.EFCore.Configurations
{
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Bio)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(c => c.Specialization)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.Rating)
                .HasColumnType("decimal(3,2)") // Ví dụ: 4.50, giới hạn 3 chữ số tổng cộng, 2 chữ số sau dấu phẩy
                .IsRequired();

            builder.Property(c => c.IsAvailable)
                .IsRequired();

            // Audit properties from BaseEntity
            builder.Property(c => c.CreatedAt)
                   .IsRequired();
            builder.Property(c => c.LastModifiedAt)
                   .IsRequired(false);

            // Mối quan hệ 1-1 hoặc 1-N với AppUser đã được cấu hình trong AppUserConfiguration
            // (AppUser HasMany Coaches, Coach HasOne User)

            // Mối quan hệ 1-N với ChatMessage
            builder.HasMany(c => c.ChatMessages)
                .WithOne(cm => cm.Coach)
                .HasForeignKey(cm => cm.CoachId)
                .IsRequired(false) // ChatMessage có thể không có Coach
                .OnDelete(DeleteBehavior.Restrict); // Không xóa Coach nếu còn tin nhắn liên quan

            // Mối quan hệ 1-N với Feedback (FeedbacksGiven)
            builder.HasMany(c => c.FeedbacksGiven)
                .WithOne() // Feedback có trường TargetEntityId và TargetEntityType, không phải CoachId cụ thể
                .HasForeignKey(f => f.TargetEntityId) // Đây là FK đến Coach.Id
                .IsRequired(); // Feedback cho Coach thì phải có TargetEntityId
            // Lưu ý: Cần thêm một điều kiện để phân biệt TargetEntityType == "COACH" trong query
            // hoặc tạo một navigation property riêng trong Feedback nếu bạn muốn mối quan hệ rõ ràng hơn.
            // Để đơn giản, tôi sẽ để mối quan hệ này là "không rõ ràng" theo FK, và dựa vào TargetEntityType để phân loại.
        }
    }
}

