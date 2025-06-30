using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.User;
using UAParser;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJWTService _jwtService;
    private readonly ApplicationDbContext _context;
    private readonly IdentitySettings _identitySettings;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IJWTService jwtService,
        ApplicationDbContext context,
        IOptions<IdentitySettings> identitySettings,
        IHttpContextAccessor httpContextAccessor
        , IUnitOfWork unitOfWork 
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _context = context;
        _identitySettings = identitySettings.Value;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
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
            CreatedAt = registerUserDto.CreateAt,
        };

        var result = await _userManager.CreateAsync(user, registerUserDto.Password);
        await _userManager.AddToRoleAsync(user, "Member");
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

        var accesstoken = await _jwtService.CreateTokenAsync(user);
        var refreshToken = await _jwtService.GenerateRefreshToken();

        var tokens = await _context.Tokens.Where(x => x.UserId == user.Id).ToListAsync(cancellationToken);
        _context.Tokens.RemoveRange(tokens);

        string? ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        string? userAgentString = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].FirstOrDefault();

        string? deviceDescription = "Unknown Device";

        if (!string.IsNullOrEmpty(userAgentString))
        {
            var uaParser = Parser.GetDefault(); 
            ClientInfo clientInfo = uaParser.Parse(userAgentString); 
            deviceDescription = $"{clientInfo.OS.Family} {clientInfo.OS.Major}"; 
            if (!string.IsNullOrEmpty(clientInfo.OS.Minor))
            {
                deviceDescription += $".{clientInfo.OS.Minor}"; 
            }
            deviceDescription += $" | {clientInfo.UA.Family} {clientInfo.UA.Major}"; 
            if (!string.IsNullOrEmpty(clientInfo.UA.Minor))
            {
                deviceDescription += $".{clientInfo.UA.Minor}"; 
            }

            if (!string.IsNullOrEmpty(clientInfo.Device.Family) && clientInfo.Device.Family != "Other")
            {
                deviceDescription += $" | {clientInfo.Device.Family}";
                if (!string.IsNullOrEmpty(clientInfo.Device.Model))
                {
                    deviceDescription += $" ({clientInfo.Device.Model})"; 
                }
            }
            else if (!string.IsNullOrEmpty(clientInfo.Device.Model))
            {
                deviceDescription += $" | {clientInfo.Device.Model}"; 
            }

            if (!string.IsNullOrEmpty(clientInfo.Device.Brand) && !string.IsNullOrEmpty(clientInfo.Device.Model))
            {
                deviceDescription = $"{clientInfo.Device.Brand} {clientInfo.Device.Model} | {clientInfo.OS.Family} | {clientInfo.UA.Family}";
            }
            else if (!string.IsNullOrEmpty(clientInfo.Device.Family) && clientInfo.Device.Family != "Other")
            {
                deviceDescription = $"{clientInfo.Device.Family} | {clientInfo.OS.Family} | {clientInfo.UA.Family}";
            }
            else
            {
                deviceDescription = $"{clientInfo.OS.Family} | {clientInfo.UA.Family}";
            }
        }


        var token = new Token();
        token.UserId = user.Id;
        token.RefreshToken = refreshToken;
        token.ExpiryDate = DateTime.UtcNow.AddDays(_identitySettings.User.ExpireInDays);
        token.IsRevoked = false;
        token.IPAddress = ipAddress;
        token.Device = deviceDescription;

        await _context.AddAsync(token, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email,
            Token = accesstoken,
            RefreshToken = refreshToken
        };
    }

    public async Task<UpdateUserResultDto> UpdateUserAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.Where(x => x.Id == updateUserDto.UserId).SingleOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new Exception($"Unable to load user with id: {updateUserDto.UserId}");
        }

        user.Email = updateUserDto.Email;
        user.DateOfBirth = updateUserDto.DateOfBirth;
        user.EmailConfirmed = updateUserDto.EmailConfirmed;
        user.FullName = updateUserDto.FullName;
        user.IsDeleted = updateUserDto.IsDeleted;
        user.LastModifiedAt = DateTime.UtcNow;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return new UpdateUserResultDto
        {
            UserId = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            PlanId = user.CurrentSubscriptionPlanId,
            EmailConfirmed = user.EmailConfirmed,
            LastModifiedAt = user.LastModifiedAt,
            DateOfBirth = user.DateOfBirth,
            IsDeleted = user.IsDeleted,
        };
    }
}
