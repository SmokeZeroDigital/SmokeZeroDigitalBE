using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Commands
{
    public class CreateProgressEntryCommandHandler : IRequestHandler<CreateProgressEntryCommand, CommandResult<ProgressEntryDto>>
    {
        private readonly IProgressEntryService _service;
        private readonly IUnitOfWork _unitOfWork;
        public CreateProgressEntryCommandHandler(IProgressEntryService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }
        public Task<CommandResult<ProgressEntryDto>> Handle(CreateProgressEntryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _service.CreateAsync(request.Entry);
                _unitOfWork.SaveAsync(cancellationToken);
                return Task.FromResult(CommandResult<ProgressEntryDto>.Success(result.Result));
            }
            catch (Exception ex)
            {
                return Task.FromResult(CommandResult<ProgressEntryDto>.Failure(ex.Message));
            }
        }
    }
}