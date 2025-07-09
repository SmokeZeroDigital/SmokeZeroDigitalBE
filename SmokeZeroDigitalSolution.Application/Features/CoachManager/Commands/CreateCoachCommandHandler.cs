namespace SmokeZeroDigitalSolution.Application.Features.CoachManager.Commands
{
    public class CreateCoachCommandHandler : IRequestHandler<CreateCoachCommand, CommandResult<CoachResponseDto>>
    {
        private readonly ICoachService _coachService;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCoachCommandHandler(ICoachService coachService, IUnitOfWork unitOfWork)
        {
            _coachService = coachService;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult<CoachResponseDto>> Handle(CreateCoachCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _coachService.CreateCoachAsync(request.Coach);
                await _unitOfWork.SaveAsync(cancellationToken);
                return CommandResult<CoachResponseDto>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<CoachResponseDto>.Failure(ex.Message);
            }
        }
    }
}