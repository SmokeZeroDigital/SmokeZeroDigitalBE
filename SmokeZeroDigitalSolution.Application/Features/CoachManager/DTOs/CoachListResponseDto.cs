namespace SmokeZeroDigitalSolution.Application.Features.CoachManager.DTOs
{
    public class CoachListResponseDto
    {
        public int Total { get; set; }
        public List<CoachResponseDto> Data { get; set; } = new();
    }
}
