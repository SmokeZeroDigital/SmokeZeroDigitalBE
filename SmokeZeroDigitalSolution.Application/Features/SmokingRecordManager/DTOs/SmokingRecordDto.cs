namespace SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.DTOs
{
    public class CreateSmokingRecordDto
    {
        public Guid UserId { get; set; }
        public int CigarettesSmoked { get; set; }
        public decimal CostIncurred { get; set; }
        public DateTime RecordDate { get; set; }
        public string? Notes { get; set; }
    }

    public class SmokingRecordDto : CreateSmokingRecordDto
    {
        public Guid Id { get; set; }
    }
}
