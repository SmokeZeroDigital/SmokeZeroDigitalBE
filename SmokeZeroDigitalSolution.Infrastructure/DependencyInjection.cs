
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Common;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Data;

namespace eCommerce_BE.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseNpgsql(configuration.GetConnectionString("MyDB")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // 2. Configure Identity
            services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); // Thêm Default Token Providers cho các tác vụ như reset password
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
            return services;
        }
    }
}
