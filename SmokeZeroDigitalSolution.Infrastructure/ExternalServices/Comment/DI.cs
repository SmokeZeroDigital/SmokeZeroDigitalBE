using SmokeZeroDigitalSolution.Application.Features.CommentManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Comment
{
    public static class DI
    {
        public static IServiceCollection RegisterCommentFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();
            return services;
        }
    }
}
