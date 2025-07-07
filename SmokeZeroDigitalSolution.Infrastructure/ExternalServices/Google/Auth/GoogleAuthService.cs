namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Google.Auth
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly string _googleClientId;

        public GoogleAuthService(IConfiguration configuration, IJWTService jwtService)
        {
            _googleClientId = configuration["Google:ClientId"];
        }

        public async Task<string> VerifyGoogleTokenAsync(string idToken, CancellationToken cancellationToken = default)
        {
            try
            {
                GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { _googleClientId }
                };
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                return payload.Email;
            }
            catch (InvalidJwtException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
