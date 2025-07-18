using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.Chat.Queries
{
    public class GetCoachesByUserIdHandler : IRequestHandler<GetCoachByUserIdQuery, QueryResult<List<UserInfoDto>>>
    {
        private readonly IConversationService _conversationService;
        public GetCoachesByUserIdHandler(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }
        public async Task<QueryResult<List<UserInfoDto>>> Handle(GetCoachByUserIdQuery request, CancellationToken cancellationToken)
        {   
            try
            {
                var result = await _conversationService.GetCoachsByUserAsync(request.UserId);
                return QueryResult<List<UserInfoDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<List<UserInfoDto>>.Failure(ex.Message);
            }
        }
    }
}
