using SmokeZeroDigitalSolution.Application.Features.Chat.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
    public class ConversationService(IConversationRepository conversationRepository) : IConversationService
    {
        private readonly IConversationRepository _conversationRepository = conversationRepository;

        public async Task<List<UserInfoDto>> GetCoachsByUserAsync(Guid userId)
        {
            return await _conversationRepository.GetCoachsByUserIdAsync(userId);
        }

        public async Task<List<UserInfoDto>> GetUsersByCoachAsync(Guid coachId)
        {
            return await _conversationRepository.GetUsersByCoachIdAsync(coachId);
        }
    }
    
}
