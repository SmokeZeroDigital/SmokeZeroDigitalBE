namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Commands
{
    public class RegisterPlanCommandHandler : IRequestHandler<RegisterPlanCommand, CommandResult<CreatePlanResultDto>>
    {
        private readonly IScriptionPlanService _scriptionPlanService;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterPlanCommandHandler(IScriptionPlanService scriptionPlanService, IUnitOfWork unitOfWork)
        {
            _scriptionPlanService = scriptionPlanService;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandResult<CreatePlanResultDto>> Handle(RegisterPlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _scriptionPlanService.CreatePlanAsync(request.Plan);
                await _unitOfWork.SaveAsync(cancellationToken);
                return CommandResult<CreatePlanResultDto>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<CreatePlanResultDto>.Failure(ex.Message);
            }
        }
    }
}
