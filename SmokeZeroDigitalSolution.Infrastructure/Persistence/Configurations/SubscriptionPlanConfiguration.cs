namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
{
    public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
            builder.HasKey(sp => sp.Id);
            builder.Property(sp => sp.Id).ValueGeneratedOnAdd();

            builder.Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sp => sp.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(sp => sp.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(sp => sp.DurationInDays)
                .IsRequired();

            builder.Property(sp => sp.IsActive)
                .IsRequired();

            // Audit properties from BaseEntity
            builder.Property(sp => sp.CreatedAt)
                   .IsRequired();
            builder.Property(sp => sp.LastModifiedAt)
                   .IsRequired(false);

            // Mối quan hệ với AppUser đã được cấu hình trong AppUserConfiguration
        }
    }
}