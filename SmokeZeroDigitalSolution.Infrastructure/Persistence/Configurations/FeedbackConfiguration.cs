namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(f => f.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(f => f.Rating)
                .IsRequired();

            builder.Property(f => f.FeedbackDate)
                .IsRequired();

            builder.Property(f => f.IsDeleted)
                .IsRequired();

            builder.Property(f => f.CreatedAt)
                   .IsRequired();
            builder.Property(f => f.LastModifiedAt)
                   .IsRequired(false);

            // Quan hệ với Coach
            builder.HasOne(f => f.Coach)
                .WithMany(c => c.Feedbacks)
                .HasForeignKey(f => f.CoachId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ với AppUser
            builder.HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}