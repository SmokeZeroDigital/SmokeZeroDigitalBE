namespace SmokeZeroDigitalSolution.Application.Features.CoachManager.Queries
{
    public class GetAllCoachesQueryHandler : IRequestHandler<GetAllCoachesQuery, QueryResult<CoachListResponseDto>>
    {
        private readonly ICoachService _coachService;
        public GetAllCoachesQueryHandler(ICoachService coachService)
        {
            _coachService = coachService;
        }
        public async Task<QueryResult<CoachListResponseDto>> Handle(GetAllCoachesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var list = await _coachService.GetAllCoachesAsync();
                var response = new CoachListResponseDto
                {
                    Total = list.Count,
                    Data = list
                };
                return QueryResult<CoachListResponseDto>.Success(response);
            }
            catch (Exception ex)
            {
                return QueryResult<CoachListResponseDto>.Failure(ex.Message);
            }
        }
    }
}