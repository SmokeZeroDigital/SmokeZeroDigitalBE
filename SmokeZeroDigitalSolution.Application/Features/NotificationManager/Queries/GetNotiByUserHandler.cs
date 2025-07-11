using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface;

namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.Queries
{
    public class GetNotiByUserHandler : IRequestHandler<GetNotiByUser, QueryResult<IEnumerable<Notification>>>
    {
        private readonly INotiService _notiService;
        public GetNotiByUserHandler(INotiService notiService)
        {
            _notiService = notiService;
        }
        public async Task<QueryResult<IEnumerable<Notification>>> Handle(GetNotiByUser request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _notiService.GetNotiByUid(request.UserId);
                return QueryResult<IEnumerable<Notification>>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<IEnumerable<Notification>>.Failure(ex.Message);
            }
        }
    }
}
