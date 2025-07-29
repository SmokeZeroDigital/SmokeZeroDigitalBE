namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
    public class CoachService : ICoachService
    {
        private readonly ICoachRepository _repo;

        public CoachService(ICoachRepository repo)
        {
            _repo = repo;
        }

        public async Task<CoachResponseDto> CreateCoachAsync(CreateCoachDto request)
        {
            var coach = new Coach
            {
                UserId = request.UserId,
                Bio = request.Bio,
                Specialization = request.Specialization,
                Rating = request.Rating,
                IsAvailable = true,
                IsActive = true
            };
            await _repo.AddAsync(coach);
            return ToResponse(coach);
        }

        public async Task<CoachResponseDto?> GetCoachByIdAsync(Guid id)
        {
            var coach = await _repo.GetByIdAsync(id);
            return coach == null ? null : ToResponse(coach);
        }

        public async Task<CoachResponseDto?> GetCoachByUserIdAsync(Guid userId)
        {
            var coach = await _repo.GetByUserIdAsync(userId);
            return coach == null ? null : ToResponse(coach);
        }

        public async Task<List<CoachResponseDto>> GetAllCoachesAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(ToResponse).ToList();
        }

        public async Task<CoachResponseDto> UpdateCoachAsync(Guid id, UpdateCoachDto request)
        {
            var coach = await _repo.GetByIdAsync(id) ?? throw new Exception("Coach not found");
            coach.Bio = request.Bio;
            coach.Specialization = request.Specialization;
            coach.IsAvailable = request.IsAvailable;
            await _repo.UpdateAsync(coach);
            return ToResponse(coach);
        }

        private CoachResponseDto ToResponse(Coach coach)
        {
            return new CoachResponseDto
            {
                Id = coach.Id,
                UserId = coach.UserId,
                FullName = coach.User?.FullName ?? "",
                Bio = coach.Bio,
                Specialization = coach.Specialization,
                Rating = coach.Rating,
                IsAvailable = coach.IsAvailable,
                IsActive = coach.IsActive
            };
        }
    }
}