using SmokeZeroDigitalSolution.Application.Features.UsersManager.Interfaces;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Data;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Services;

namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Identity
{
    public static class DI
    {
        public static IServiceCollection RegisterSecurityManager(this IServiceCollection services, IConfiguration configuration)
        {
            var aspNetIdentitySectionName = "AspNetIdentity";

            services.Configure<IdentitySettings>(configuration.GetSection(aspNetIdentitySectionName));

            services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                var identitySettings = configuration.GetSection(aspNetIdentitySectionName).Get<IdentitySettings>();
                if (identitySettings == null)
                {
                    throw new Exception($"{aspNetIdentitySectionName} configuration section at appsettings.json is missing");
                }

                // Password settings
                options.Password.RequireDigit = identitySettings.Password.RequireDigit;
                options.Password.RequireLowercase = identitySettings.Password.RequireLowercase;
                options.Password.RequireUppercase = identitySettings.Password.RequireUppercase;
                options.Password.RequireNonAlphanumeric = identitySettings.Password.RequireNonAlphanumeric;
                options.Password.RequiredLength = identitySettings.Password.RequiredLength;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identitySettings.Lockout.DefaultLockoutTimeSpanInMinutes);
                options.Lockout.MaxFailedAccessAttempts = identitySettings.Lockout.MaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = identitySettings.Lockout.AllowedForNewUsers;

                // User settings
                options.User.RequireUniqueEmail = identitySettings.User.RequireUniqueEmail;

                // SignIn settings
                options.SignIn.RequireConfirmedEmail = identitySettings.SignIn.RequireConfirmedEmail;

            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 403;
                    return Task.CompletedTask;
                };
            });
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
