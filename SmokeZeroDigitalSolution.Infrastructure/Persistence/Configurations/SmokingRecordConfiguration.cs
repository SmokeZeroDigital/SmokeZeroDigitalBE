namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Configurations
{
    public class SmokingRecordConfiguration : IEntityTypeConfiguration<SmokingRecord>
    {
        public void Configure(EntityTypeBuilder<SmokingRecord> builder)
        {
            builder.HasKey(sr => sr.Id);
            builder.Property(sr => sr.Id).ValueGeneratedOnAdd();

            builder.Property(sr => sr.CigarettesSmoked)
                .IsRequired();

            builder.Property(sr => sr.CostIncurred)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(sr => sr.RecordDate)
                .IsRequired();

            builder.Property(sr => sr.Notes)
                .HasMaxLength(500)
                .IsRequired(false);

            // Audit properties from BaseEntity
            builder.Property(sr => sr.CreatedAt)
                   .IsRequired();
            builder.Property(sr => sr.LastModifiedAt)
                   .IsRequired(false);

            // Mối quan hệ với AppUser đã được cấu hình trong AppUserConfiguration
        }
    }
}