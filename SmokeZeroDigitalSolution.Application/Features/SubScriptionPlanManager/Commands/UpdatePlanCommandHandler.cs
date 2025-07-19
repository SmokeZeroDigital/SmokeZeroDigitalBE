namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Commands
{
    public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, CommandResult<GetPlanResponseDto>>
    {
        private readonly IScriptionPlanService _scriptionPlanService;
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePlanCommandHandler(IScriptionPlanService scriptionPlanService, IUnitOfWork unitOfWork)
        {
            _scriptionPlanService = scriptionPlanService;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandResult<GetPlanResponseDto>> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _scriptionPlanService.UpdatePlanAsync(request.Plan);
                await _unitOfWork.SaveAsync(cancellationToken);
                return CommandResult<GetPlanResponseDto>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<GetPlanResponseDto>.Failure(ex.Message);
            }
        }
    }
}
