using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Blog;

namespace SmokeZeroDigitalSolution.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDataAccess(configuration);
            services.RegisterToken(configuration);
            services.RegisterSecurityManager(configuration);
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<ICoachRepository, CoachRepository>();
            services.AddScoped<ICoachService, CoachService>();
            services.RegisterBlogFeature(configuration);
            services.RegisterCommentFeature(configuration);
            services.RegisterPaymentPlan(configuration);
            services.RegisterGooglePlan(configuration);
            services.RegisterChatRealTime();
            return services;
        }
    }
}
