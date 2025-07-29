using Microsoft.AspNetCore.Mvc;
using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.Exceptions;

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
    private readonly IEmailService _emailService;

    public AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IJWTService jwtService,
        ApplicationDbContext context,
        IOptions<IdentitySettings> identitySettings,
        IHttpContextAccessor httpContextAccessor
        , IUnitOfWork unitOfWork,
        IEmailService emailService
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _context = context;
        _identitySettings = identitySettings.Value;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }
    public async Task<RegisterResultDto> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken = default)
    {
        if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            throw new Exception("Password and confirmation do not match.");

        // Create a new user
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

        // Create the user in the database
        var result = await _userManager.CreateAsync(user, registerUserDto.Password);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        // Generate a 6-digit numeric email confirmation token
        var numericToken = await GenerateEmailConfirmationTokenAsync(user);

        // Save the token in the database with an expiration time
        var tokenExpiry = DateTime.UtcNow.AddMinutes(15); // Token valid for 15 minutes
        await SaveTokenAsync(user.Id, numericToken, tokenExpiry);

        // Return the registration result
        return new RegisterResultDto
        {
            UserId = user.Id, // Add UserId so frontend can use it for email confirmation
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
        if (user == null || user.IsDeleted == true)
            throw new Exception("Invalid credentials.");

        if (!user.EmailConfirmed)
        {
            // Generate a new OTP and send it to the user
            await ResendConfirmationTokenAsync(user);
            throw new UnconfirmedEmailException("Email not confirmed. A new confirmation code has been sent to your email.")
            {
                UserId = user.Id,
                Email = user.Email
            };
        }

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
            UserName = user.UserName,
            RefreshToken = refreshToken,
            PlanId = user.CurrentSubscriptionPlanId
        };
    }
    public async Task<AuthResponseDto> GoogleLoginAsync(AppUser user, CancellationToken cancellationToken = default)
    {
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

        var emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        await _userManager.ConfirmEmailAsync(user, emailConfirmToken);

        await _context.AddAsync(token, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email,
            Token = accesstoken,
            UserName = user.UserName,
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

        // Only update fields that are provided (not null)
        if (updateUserDto.Email != null)
            user.Email = updateUserDto.Email;

        if (updateUserDto.DateOfBirth.HasValue)
            user.DateOfBirth = updateUserDto.DateOfBirth;

        if (updateUserDto.FullName != null)
            user.FullName = updateUserDto.FullName;

        if (updateUserDto.IsDeleted.HasValue)
            user.IsDeleted = updateUserDto.IsDeleted;

        if (updateUserDto.PlanId.HasValue)
            user.CurrentSubscriptionPlanId = updateUserDto.PlanId;

        user.EmailConfirmed = updateUserDto.EmailConfirmed; // If you want to keep this, otherwise check for a flag

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
    public async Task<bool> DeleteAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return false;
        user.IsDeleted = true;
        var tokens = await _context.Tokens.Where(t => t.UserId == user.Id).ToListAsync(cancellationToken);
        _context.Tokens.RemoveRange(tokens);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
    public async Task<ConfirmEmailResultDto> ConfirmEmailAsync(string email, string code)
    {
        if (string.IsNullOrWhiteSpace(email))
            return new ConfirmEmailResultDto { Success = false, Message = "Email cannot be empty." };

        if (string.IsNullOrWhiteSpace(code))
            return new ConfirmEmailResultDto { Success = false, Message = "Confirmation code is required." };

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return new ConfirmEmailResultDto { Success = false, Message = "User not found." };

        if (user.EmailConfirmed)
            return new ConfirmEmailResultDto { Success = true, Message = "Email already confirmed." };

        // Validate the token using the same logic as ConfirmEmailWithTokenAsync
        if (!await ValidateTokenAsync(user.Id, code))
            return new ConfirmEmailResultDto { Success = false, Message = "Invalid or expired token." };

        // Confirm the email
        user.EmailConfirmed = true;
        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
            return new ConfirmEmailResultDto { Success = false, Message = $"Failed to confirm email: {errors}" };
        }

        // Assign the "Member" role after email confirmation
        if (!await _userManager.IsInRoleAsync(user, "Member"))
        {
            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            if (!roleResult.Succeeded)
            {
                var error = roleResult.Errors.FirstOrDefault()?.Description ?? "Unknown error while assigning role.";
                return new ConfirmEmailResultDto { Success = false, Message = $"Email confirmed, but failed to assign role: {error}" };
            }
        }

        // Revoke the token after successful confirmation
        await RevokeTokenAsync(user.Id, code);

        return new ConfirmEmailResultDto { Success = true, Message = "Email confirmed successfully." };
    }

    public async Task<ConfirmEmailResultDto> ConfirmEmailWithTokenAsync(Guid userId, string token)
    {
        // Validate input parameters
        if (userId == Guid.Empty)
            return new ConfirmEmailResultDto { Success = false, Message = "Invalid user ID." };

        if (string.IsNullOrWhiteSpace(token))
            return new ConfirmEmailResultDto { Success = false, Message = "Token is required." };

        // Validate the token first
        if (!await ValidateTokenAsync(userId, token))
            return new ConfirmEmailResultDto { Success = false, Message = "Invalid or expired token." };

        // Find the user
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return new ConfirmEmailResultDto { Success = false, Message = "User not found." };

        // Check if user is already confirmed
        if (user.EmailConfirmed)
            return new ConfirmEmailResultDto { Success = true, Message = "Email already confirmed." };

        // Confirm the email
        user.EmailConfirmed = true;
        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
            return new ConfirmEmailResultDto { Success = false, Message = $"Failed to confirm email: {errors}" };
        }

        // Assign the "Member" role after email confirmation
        if (!await _userManager.IsInRoleAsync(user, "Member"))
        {
            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            if (!roleResult.Succeeded)
            {
                var error = roleResult.Errors.FirstOrDefault()?.Description ?? "Unknown error while assigning role.";
                return new ConfirmEmailResultDto { Success = false, Message = $"Email confirmed, but failed to assign role: {error}" };
            }
        }

        // Revoke the token after successful confirmation
        await RevokeTokenAsync(userId, token);

        return new ConfirmEmailResultDto { Success = true, Message = "Email confirmed successfully." };
    }

    public async Task RevokeTokenAsync(Guid userId, string token)
    {
        var tokenEntity = await _context.Tokens
            .Where(t => t.UserId == userId && t.RefreshToken == token)
            .FirstOrDefaultAsync();

        if (tokenEntity != null)
        {
            tokenEntity.IsRevoked = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ValidateTokenAsync(Guid userId, string submittedToken)
    {
        var tokenEntity = await _context.Tokens
            .Where(t => t.UserId == userId && t.RefreshToken == submittedToken)
            .FirstOrDefaultAsync();

        if (tokenEntity == null)
            return false; // Token not found

        if (tokenEntity.ExpiryDate < DateTime.UtcNow)
            return false; // Token expired

        if (tokenEntity.IsRevoked)
            return false; // Token already used or revoked

        return true; // Token is valid
    }

    public async Task SaveTokenAsync(Guid userId, string token, DateTime expiry)
    {
        var tokenEntity = new Token
        {
            UserId = userId,
            RefreshToken = token, // Reuse the `RefreshToken` field for email confirmation tokens
            ExpiryDate = expiry,
            IsRevoked = false,
            IPAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString(),
            Device = "Email Confirmation" // Mark this as an email confirmation token
        };

        await _context.Tokens.AddAsync(tokenEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
    {
        var numericToken = GenerateNumericToken();
        var subject = "Email Confirmation";
        var message = $"Here is your email confirmation code: {numericToken}";
        
        if (!string.IsNullOrEmpty(user.Email))
        {
            await _emailService.SendAsync(user.Email, subject, message);
        }
        
        return numericToken;
    }

    private string GenerateNumericToken()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }

    public async Task ResendConfirmationTokenAsync(AppUser user)
    {
        // Generate a new 6-digit numeric email confirmation token
        var numericToken = await GenerateEmailConfirmationTokenAsync(user);

        // Save the token in the database with an expiration time
        var tokenExpiry = DateTime.UtcNow.AddMinutes(15); // Token valid for 15 minutes
        await SaveTokenAsync(user.Id, numericToken, tokenExpiry);
    }
}