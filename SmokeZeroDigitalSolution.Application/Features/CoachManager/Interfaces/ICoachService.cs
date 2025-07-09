namespace SmokeZeroDigitalSolution.Application.Features.CoachManager.Interfaces
{
    public interface ICoachService
    {
        Task<CoachResponseDto> CreateCoachAsync(CreateCoachDto request);
        Task<CoachResponseDto?> GetCoachByIdAsync(Guid id);
        Task<List<CoachResponseDto>> GetAllCoachesAsync();
        Task<CoachResponseDto> UpdateCoachAsync(Guid id, UpdateCoachDto request);
        Task<CoachResponseDto?> GetCoachByUserIdAsync(Guid userId);
    }
}
