using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using SmokeZeroDigitalSolution.Application.Interfaces;
using System.Text;
using System.Text.Json;

namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.JWT
{
    public static class DI
    {
        public static IServiceCollection RegisterToken(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSectionName = "JWT";
            services.Configure<TokenSettings>(configuration.GetSection(jwtSectionName));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var tokenSettings = configuration.GetSection(jwtSectionName).Get<TokenSettings>();

                if (tokenSettings?.Key.Length < 32)
                {
                    throw new Exception("JWT key should be minimal 32 character");
                }

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromMinutes(tokenSettings?.ClockSkewInMinute ?? 5),
                    ValidIssuer = tokenSettings?.Issuer,
                    ValidAudience = tokenSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings?.Key ?? throw new ArgumentNullException("JWT Key is empty")))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.HttpContext.Request.Cookies["accessToken"];
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        else
                        {
                            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                            {
                                context.Token = authorizationHeader.Substring("Bearer ".Length).Trim();
                            }
                        }

                        return Task.CompletedTask;
                    },

                    OnChallenge = context =>
                    {
                        if (context.AuthenticateFailure is SecurityTokenExpiredException)
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 498;
                            context.Response.ContentType = "application/json";

                            var result = JsonSerializer.Serialize(new
                            {
                                code = 498,
                                message = "Token has expired.",
                                error = new
                                {
                                    @ref = "https://datatracker.ietf.org/doc/html/rfc9110",
                                    exceptionType = "SecurityTokenExpiredException",
                                    innerException = "SecurityTokenExpiredException",
                                    source = "",
                                    stackTrace = ""
                                }
                            });

                            return context.Response.WriteAsync(result);
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddTransient<IJWTService, JWTService>();
            services.AddScoped<TokenSettings>();
            return services;
        }
    }
}
