namespace SmokeZeroDigitalSolution.Application.Features.CoachManager.Queries
{
    public class GetCoachByIdQueryHandler : IRequestHandler<GetCoachByIdQuery, QueryResult<CoachResponseDto>>
    {
        private readonly ICoachService _coachService;
        public GetCoachByIdQueryHandler(ICoachService coachService)
        {
            _coachService = coachService;
        }
        public async Task<QueryResult<CoachResponseDto>> Handle(GetCoachByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _coachService.GetCoachByIdAsync(request.Id);
                if (result == null) return QueryResult<CoachResponseDto>.Failure("Coach not found");
                return QueryResult<CoachResponseDto>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<CoachResponseDto>.Failure(ex.Message);
            }
        }
    }
}