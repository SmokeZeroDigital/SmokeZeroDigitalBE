﻿using System.Security.Cryptography;

namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.JWT
{
    public class JWTService : IJWTService
    {
        private readonly TokenSettings _tokenSettings;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;

        public JWTService(IOptions<TokenSettings> tokenSettings, UserManager<AppUser> userManager)
        {
            _tokenSettings = tokenSettings.Value;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key));
            _userManager = userManager;
        }

        public async Task<string> CreateTokenAsync(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", user.FullName),
                new Claim(ClaimTypes.Gender, user.Gender.ToString()),
                new Claim("UserId" , user.Id.ToString()),
                new Claim("BirthDate", user.DateOfBirth.ToString()),
                new Claim("Username", user.UserName.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpireInMinute),
                NotBefore = DateTime.UtcNow,
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = creds,
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.Audience
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
