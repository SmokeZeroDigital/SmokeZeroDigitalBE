namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
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

            builder.Property(cm => cm.MessageType)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(cm => cm.IsRead)
                .IsRequired();

            // Audit properties from BaseEntity
            builder.Property(cm => cm.CreatedAt)
                   .IsRequired();
            builder.Property(cm => cm.LastModifiedAt)
                   .IsRequired(false);

            // Quan hệ với Conversation
            builder.HasOne(cm => cm.Conversation)
                .WithMany(c => c.ChatMessages)
                .HasForeignKey(cm => cm.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ với User
            builder.HasOne(cm => cm.User)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(cm => cm.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}