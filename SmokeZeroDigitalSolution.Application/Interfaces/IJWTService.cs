namespace SmokeZeroDigitalSolution.Application.Interfaces
{
    public interface IJWTService
    {
        Task<string> CreateTokenAsync(AppUser user);
        Task<string> GenerateRefreshToken();
    }
}
