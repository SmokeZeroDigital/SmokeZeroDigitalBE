using SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Commands
{
    public class CreateProgressEntryCommand : IRequest<CommandResult<ProgressEntryDto>>
    {
        public CreateProgressEntryDto Entry { get; set; } = default!;
    }
}
