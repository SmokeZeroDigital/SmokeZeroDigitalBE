using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
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
            builder.Property(c => c.IsActive)
                .IsRequired();

            // Audit properties from BaseEntity
            builder.Property(c => c.CreatedAt)
                   .IsRequired();
            builder.Property(c => c.LastModifiedAt)
                   .IsRequired(false);

            // Quan hệ 1-1 với AppUser
            builder.HasOne(c => c.User)
                .WithOne(u => u.Coach)
                .HasForeignKey<Coach>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(c => c.UserId).IsUnique();

            // Quan hệ 1-n với Feedback
            builder.HasMany(c => c.Feedbacks)
                .WithOne(f => f.Coach)
                .HasForeignKey(f => f.CoachId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ 1-n với Conversation
            builder.HasMany(c => c.Conversations)
                .WithOne(conv => conv.Coach)
                .HasForeignKey(conv => conv.CoachId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

