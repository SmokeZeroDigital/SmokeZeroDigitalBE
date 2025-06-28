namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJWTService _jwtService;
    private readonly ApplicationDbContext _context;
    private readonly IdentitySettings _identitySettings;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IJWTService jwtService,
        ApplicationDbContext context,
        IOptions<IdentitySettings> identitySettings,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _context = context;
        _identitySettings = identitySettings.Value;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<RegisterResultDto> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken = default)
    {
        if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            throw new Exception("Password and confirmation do not match.");

        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            Email = registerUserDto.Email,
            UserName = registerUserDto.Username,
            FullName = registerUserDto.FullName,
            DateOfBirth = registerUserDto.DateOfBirth,
            Gender = registerUserDto.Gender,
        };

        var result = await _userManager.CreateAsync(user, registerUserDto.Password);

        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        return new RegisterResultDto
        {
            Email = user.Email,
            FullName = user.FullName,
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender,
            CreateAt = user.CreatedAt
        };
    }
    public async Task<AuthResponseDto> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null) throw new Exception("Invalid credentials.");

        var result = await _signInManager.PasswordSignInAsync(user, password, true, false);
        if (!result.Succeeded) throw new Exception("Invalid login attempt.");

        var token = await _jwtService.CreateTokenAsync(user);

        return new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email,
            Token = token,
        };
    }
}
