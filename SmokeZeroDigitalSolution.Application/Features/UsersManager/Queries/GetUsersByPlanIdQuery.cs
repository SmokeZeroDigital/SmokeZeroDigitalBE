namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries
{
    public class GetUsersByPlanIdQuery : IRequest<QueryResult<List<AppUser>>>
    {
        public Guid PlanId { get; set; }
    }
}
