using Microsoft.AspNetCore.Identity;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries
{
    public class GoogleLoginQueryHandler : IRequestHandler<GoogleLoginQuery, QueryResult<AuthResponseDto>>
    {
        private readonly IGoogleAuthService _googleAuthService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;

        public GoogleLoginQueryHandler(
            IGoogleAuthService googleAuthService,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IAuthService authService)
        {
            _googleAuthService = googleAuthService;
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }

        public async Task<QueryResult<AuthResponseDto>> Handle(GoogleLoginQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.User.Token))
                return QueryResult<AuthResponseDto>.Failure("Google ID token không được để trống.");

            var email = await _googleAuthService.VerifyGoogleTokenAsync(request.User.Token, cancellationToken);
            if (email == null)
                return QueryResult<AuthResponseDto>.UnauthorizedResult("Invalid Google token");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = email,
                    Email = email,
                };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                    return QueryResult<AuthResponseDto>.Failure("Failed to create user.");

                await _userManager.AddToRoleAsync(user, "Customer");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.ConfirmEmailAsync(user, token);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            var dto = await _authService.GoogleLoginAsync(user, cancellationToken);

            return QueryResult<AuthResponseDto>.Success(dto);
        }
    }
}