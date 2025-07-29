using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface;

namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.Queries
{
    public class GetNotiByIdHandler : IRequestHandler<GetNotiById, QueryResult<Notification>>
    
    {
        private readonly INotiService _notiService;
        public GetNotiByIdHandler(INotiService notiService)
        {
            _notiService = notiService;
        }
        public async Task<QueryResult<Notification>> Handle(GetNotiById request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _notiService.GetNotificationByIdAsync(request.Id);
                return QueryResult<Notification>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<Notification>.Failure(ex.Message);
            }
        }
    }
}
