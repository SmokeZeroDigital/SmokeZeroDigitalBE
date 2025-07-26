using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using SmokeZeroDigitalSolution.Application.Features.NotificationManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Services;

namespace SmokeZeroDigitalProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
             .AddGoogle(options =>
             {
                 options.ClientId = builder.Configuration["Google:ClientId"];
                 options.ClientSecret = builder.Configuration["Google:ClientSecret"];
                 options.AccessType = "offline";
                 options.Scope.Add("profile");
                 options.Scope.Add("email");
                 options.SaveTokens = true;
                 options.CallbackPath = "/signin-google";
                 options.Events.OnCreatingTicket = context =>
                 {
                     // Optional: Log để debug
                     Console.WriteLine($"Google auth callback received");
                     return Task.CompletedTask;
                 };
             });

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new FlexibleDateTimeConverterFactory("yyyy-MM-ddTHH:mm:ss.fffZ", alwaysAssumeUtcOnRead: true));
                    options.JsonSerializerOptions.Converters.Add(new FlexibleDateTimeConverterFactory("yyyy-MM-dd", alwaysAssumeUtcOnRead: true));
                });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.RegisterSwagger();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddExceptionHandler<CustomExceptionHandler>();
            builder.Services.AddScoped<IRequestExecutor, RequestExecutor>();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout 30 phút
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.None; // Cho phép cross-site request từ VNPay
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            //builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("Jwt"));

            builder.Services.RegisterChatRealTime(builder.Configuration);

            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddHostedService<NotificationBackgroundService>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            builder.Services.AddSingleton<ApiConfig>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseMiddleware<GlobalApiExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("vi-VN"),
                SupportedCultures = new List<CultureInfo> { new CultureInfo("vi-VN") },
                SupportedUICultures = new List<CultureInfo> { new CultureInfo("vi-VN") }
            });
            app.UseSession();
            app.MapRazorPages();
            app.MapControllers();
            app.MapHub<ChatHub>("/hubs/chat");
            app.Run();
        }
    }
}