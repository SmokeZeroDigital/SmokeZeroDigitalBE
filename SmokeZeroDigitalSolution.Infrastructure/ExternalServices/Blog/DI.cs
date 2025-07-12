using SmokeZeroDigitalSolution.Application.Features.BlogManager.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Blog
{
    public static class DI
    {
        public static IServiceCollection RegisterBlogFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogService, BlogService>();
            return services;
        }
    }
}
