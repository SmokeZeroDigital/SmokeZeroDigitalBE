namespace SmokeZeroDigitalSolution.Application.Interfaces
{
    public interface IGoogleAuthService
    {
        Task<string> VerifyGoogleTokenAsync(string idToken, CancellationToken cancellationToken = default);
    }
}
