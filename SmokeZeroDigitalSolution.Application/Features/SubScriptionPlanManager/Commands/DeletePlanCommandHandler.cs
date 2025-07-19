using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Commands
{
    public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, CommandResult<bool>>
    {
        private readonly IScriptionPlanService _scriptionPlanService;
        private readonly IUnitOfWork _unitOfWork;
        public DeletePlanCommandHandler(IScriptionPlanService scriptionPlanService, IUnitOfWork unitOfWork)
        {
            _scriptionPlanService = scriptionPlanService;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandResult<bool>> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _scriptionPlanService.DeletePlanAsync(request.PlanId);
                await _unitOfWork.SaveAsync(cancellationToken);
                return CommandResult<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<bool>.Failure(ex.Message);
            }
        }
    }
}
