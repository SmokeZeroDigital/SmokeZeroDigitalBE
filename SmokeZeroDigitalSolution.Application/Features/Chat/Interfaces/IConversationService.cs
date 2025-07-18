using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces
{
    public interface IConversationService
    {
        Task<List<UserInfoDto>> GetUsersByCoachAsync(Guid coachId);
        Task<List<UserInfoDto>> GetCoachsByUserAsync(Guid userId);
    }
}
