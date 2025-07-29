namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
{
    public class ProgressEntryConfiguration : IEntityTypeConfiguration<ProgressEntry>
    {
        public void Configure(EntityTypeBuilder<ProgressEntry> builder)
        {
            builder.HasKey(pe => pe.Id);
            builder.Property(pe => pe.Id).ValueGeneratedOnAdd();

            builder.Property(pe => pe.EntryDate)
                .IsRequired();

            builder.Property(pe => pe.CigarettesSmokedToday)
                .IsRequired();

            builder.Property(pe => pe.MoneySavedToday)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(pe => pe.HealthStatusNotes)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(pe => pe.CravingLevel)
                .IsRequired();

            builder.Property(pe => pe.Challenges)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(pe => pe.Successes)
                .HasMaxLength(500)
                .IsRequired(false);

            // Audit properties from BaseEntity
            builder.Property(pe => pe.CreatedAt)
                   .IsRequired();
            builder.Property(pe => pe.LastModifiedAt)
                   .IsRequired(false);

            builder.HasOne(pe => pe.User)
                .WithMany(u => u.ProgressEntries)
                .HasForeignKey(pe => pe.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
