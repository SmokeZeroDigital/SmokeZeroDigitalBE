using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface;

namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.Queries
{
    public class GetAllNotiHandler : IRequestHandler<GetAllNoti, QueryResult<IQueryable<Notification>>>
    {
        private readonly INotiService _notiService;
        public GetAllNotiHandler(INotiService notiService)
        {
            _notiService = notiService;
        }
        public async Task<QueryResult<IQueryable<Notification>>> Handle(GetAllNoti request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _notiService.GetAllAsync();
                return QueryResult<IQueryable<Notification>>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<IQueryable<Notification>>.Failure(ex.Message);
            }
        }
    }

}
