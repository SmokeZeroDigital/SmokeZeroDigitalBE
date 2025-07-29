namespace SmokeZeroDigitalSolution.Application.Features.CoachManager.Queries
{
    public class GetCoachByIdQuery : IRequest<QueryResult<CoachResponseDto>>
    {
        public Guid Id { get; set; }
    }
}