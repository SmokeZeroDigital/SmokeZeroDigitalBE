using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.Chat.Queries
{
    public class GetUsersByCoachIdQuery : IRequest<QueryResult<List<UserInfoDto>>>
    {
        public Guid CoachId { get; init; } = default!;
    }
}
