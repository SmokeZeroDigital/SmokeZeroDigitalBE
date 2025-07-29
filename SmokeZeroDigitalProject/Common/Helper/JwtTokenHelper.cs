using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SmokeZeroDigitalProject.Common.Helper
{

    public static class JwtTokenHelper
    {
        public static List<Claim> DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            return jwt.Claims.ToList();
        }

        public static string? GetClaim(string token, string claimType)
        {
            var claims = DecodeToken(token);
            return claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }

        public static void LogAllClaims(string token)
        {
            var claims = DecodeToken(token);
            Console.WriteLine("📜 JWT Claims:");
            foreach (var claim in claims)
            {
                Console.WriteLine($"➡️ {claim.Type} = {claim.Value}");
            }
        }
    }
}