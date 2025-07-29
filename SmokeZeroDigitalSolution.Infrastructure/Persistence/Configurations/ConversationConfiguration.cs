using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.LastMessage)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(c => c.LastMessageSender)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(c => c.IsActive)
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .IsRequired();
            builder.Property(c => c.LastModifiedAt)
                .IsRequired(false);

            // Quan hệ với Coach
            builder.HasOne(c => c.Coach)
                .WithMany(co => co.Conversations)
                .HasForeignKey(c => c.CoachId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ với AppUser
            builder.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ 1-n với ChatMessages
            builder.HasMany(c => c.ChatMessages)
                .WithOne(cm => cm.Conversation)
                .HasForeignKey(cm => cm.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}