using SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager;
namespace SmokeZeroDigitalSolution.Infrastructure.DataAccessManager.EFCore.Repositories
{
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        // private readonly IJwtTokenGenerator _jwtTokenGenerator; // Nếu bạn có JWT

        public SecurityService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager /*, IJwtTokenGenerator jwtTokenGenerator */)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            // _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResultDto> RegisterUserAsync(RegisterRequest request)
        {
            var user = new AppUser
            {
                UserName = request.Email, // Email thường dùng làm UserName
                Email = request.Email,
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                RegistrationDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow // Đảm bảo gán CreatedAt ở đây hoặc trong DbContext SaveChanges
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // Gán vai trò mặc định cho người dùng mới, ví dụ "Customer"
                var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
                if (!roleResult.Succeeded)
                {
                    // Xử lý lỗi nếu không thể gán vai trò
                    return new LoginResultDto
                    {
                        Succeeded = false,
                        Errors = roleResult.Errors.Select(e => e.Description).ToList(),
                        Message = "User registered but failed to assign role."
                    };
                }

                // Tự động đăng nhập sau khi đăng ký (tùy chọn)
                // await _signInManager.SignInAsync(user, isPersistent: false);

                // Tạo JWT token nếu cần
                // var token = _jwtTokenGenerator.GenerateToken(user);

                return new LoginResultDto
                {
                    Succeeded = true,
                    Message = "User registered successfully.",
                    // Token = token,
                    UserId = user.Id,
                    UserName = user.UserName
                };
            }
            else
            {
                return new LoginResultDto
                {
                    Succeeded = false,
                    Errors = result.Errors.Select(e => e.Description).ToList(),
                    Message = "User registration failed."
                };
            }
        }

        public async Task<LoginResultDto> LoginUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new LoginResultDto { Succeeded = false, Message = "Invalid credentials." };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Tạo JWT token nếu cần
                // var token = _jwtTokenGenerator.GenerateToken(user);

                return new LoginResultDto
                {
                    Succeeded = true,
                    Message = "Login successful.",
                    // Token = token,
                    UserId = user.Id,
                    UserName = user.UserName
                };
            }
            else
            {
                return new LoginResultDto { Succeeded = false, Message = "Invalid credentials." };
            }
        }
    }
}