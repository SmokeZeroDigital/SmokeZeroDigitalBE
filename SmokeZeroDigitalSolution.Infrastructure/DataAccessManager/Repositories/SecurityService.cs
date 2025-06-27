using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager;
using SmokeZeroDigitalSolution.Domain.Entites;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmokeZeroDigitalSolution.Infrastructure.DataAccessManager.Repositories
{
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        public SecurityService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<LoginResultDto> RegisterAsync(RegisterRequest request)
        {
            var user = new AppUser { UserName = request.UserName, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return new LoginResultDto
                {
                    Token = null,
                    UserName = user.UserName,
                    Email = user.Email,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }
            // Đăng nhập luôn sau khi đăng ký thành công
            var token = GenerateJwtToken(user);
            return new LoginResultDto
            {
                Token = token,
                UserName = user.UserName,
                Email = user.Email,
                Errors = null
            };
        }

        public async Task<LoginResultDto> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new LoginResultDto { Token = null, UserName = null, Email = null, Errors = new List<string> { "Tài khoản không tồn tại" } };
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                return new LoginResultDto { Token = null, UserName = user.UserName, Email = user.Email, Errors = new List<string> { "Sai mật khẩu" } };
            }
            var token = GenerateJwtToken(user);
            return new LoginResultDto { Token = token, UserName = user.UserName, Email = user.Email, Errors = null };
        }

        private string GenerateJwtToken(AppUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}