namespace SmokeZeroDigitalSolution.Infrastructure.DataAccessManager.EFCore.Configurations
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(cm => cm.Id);
            builder.Property(cm => cm.Id).ValueGeneratedOnAdd();

            builder.Property(cm => cm.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(cm => cm.Timestamp)
                .IsRequired();

            builder.Property(cm => cm.IsRead)
                .IsRequired();

            // Audit properties from BaseEntity
            builder.Property(cm => cm.CreatedAt)
                   .IsRequired();
            builder.Property(cm => cm.LastModifiedAt)
                   .IsRequired(false);

            // Mối quan hệ với SenderUser đã được cấu hình trong AppUserConfiguration
            // Mối quan hệ với ReceiverUser đã được cấu hình trong AppUserConfiguration

            // Mối quan hệ 1-N với Coach (nếu tin nhắn được gửi đến/từ một Coach)
            builder.HasOne(cm => cm.Coach)
                .WithMany(c => c.ChatMessages)
                .HasForeignKey(cm => cm.CoachId)
                .IsRequired(false) // Có thể là chat giữa 2 AppUser, không liên quan đến Coach
                .OnDelete(DeleteBehavior.Restrict); // Không xóa Coach nếu còn tin nhắn liên quan
        }
    }
}