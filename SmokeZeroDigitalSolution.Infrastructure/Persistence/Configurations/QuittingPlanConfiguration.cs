namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
{
    public class QuittingPlanConfiguration : IEntityTypeConfiguration<QuittingPlan>
    {
        public void Configure(EntityTypeBuilder<QuittingPlan> builder)
        {
            builder.HasKey(qp => qp.Id);
            builder.Property(qp => qp.Id).ValueGeneratedOnAdd();

            builder.Property(qp => qp.ReasonToQuit)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(qp => qp.StartDate)
                .IsRequired();

            builder.Property(qp => qp.ExpectedEndDate)
                .IsRequired();

            builder.Property(qp => qp.Stages)
                .HasColumnType("text") // Lưu JSON hoặc TEXT
                .IsRequired(false);

            builder.Property(qp => qp.CustomNotes)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(qp => qp.InitialCigarettesPerDay)
                .IsRequired();

            builder.Property(qp => qp.InitialCostPerCigarette)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(qp => qp.IsActive)
                .IsRequired();

            // Audit properties from BaseEntity
            builder.Property(qp => qp.CreatedAt)
                   .IsRequired();
            builder.Property(qp => qp.LastModifiedAt)
                   .IsRequired(false);

            builder.HasOne(qp => qp.User)
                .WithMany(u => u.QuittingPlans)
                .HasForeignKey(qp => qp.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}