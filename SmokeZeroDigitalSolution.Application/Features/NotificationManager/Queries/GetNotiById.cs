namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.Queries
{
    public class GetNotiById : IRequest<QueryResult<Notification>>
    {
        public Guid Id { get; set; }
    }
   
}
