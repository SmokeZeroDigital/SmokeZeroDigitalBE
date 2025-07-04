﻿using SmokeZeroDigitalProject.Common.Realtime;
using SmokeZeroDigitalSolution.Application.Common.Interfaces;
using SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces;
using SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories;

namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Chat
{
    public static class DI
    {
        public static IServiceCollection RegisterChatRealTime(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSignalR();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IChatNotifier, ChatNotifier>();
            services.AddHttpClient("ChatClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7146");
            });
            return services;
        }
    }
}
