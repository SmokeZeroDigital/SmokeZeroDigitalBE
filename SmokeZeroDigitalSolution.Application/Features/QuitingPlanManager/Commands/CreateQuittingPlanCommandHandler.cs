using SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.Interfaces;
using SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.Commands;
using SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.QuitingPlanManager.Commands
{
    public class CreateQuittingPlanCommandHandler : IRequestHandler<CreateQuittingPlanCommand, CommandResult<QuittingPlanDto>>
    {
        private readonly IQuittingPlanService _service;
        private readonly IUnitOfWork _unitOfWork;

        public CreateQuittingPlanCommandHandler(IQuittingPlanService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult<QuittingPlanDto>> Handle(CreateQuittingPlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.CreateAsync(request.QuittingPlan);
                await _unitOfWork.SaveAsync(cancellationToken);
                return CommandResult<QuittingPlanDto>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<QuittingPlanDto>.Failure(ex.Message);
            }
        }
    }

}
