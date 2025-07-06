using SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces;

namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Chat
{
    public static class DI
    {
        public static IServiceCollection RegisterChatRealTime(this IServiceCollection services)
        {
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            return services;
        }
    }
}
