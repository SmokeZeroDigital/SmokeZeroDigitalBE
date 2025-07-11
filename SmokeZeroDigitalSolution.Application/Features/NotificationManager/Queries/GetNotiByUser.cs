namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.Queries
{
    public class GetNotiByUser : IRequest<QueryResult<IEnumerable<Notification>>>
    {
        public Guid UserId { get; set; }
    }
    
}
