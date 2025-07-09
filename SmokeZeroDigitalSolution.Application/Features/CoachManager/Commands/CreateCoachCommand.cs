namespace SmokeZeroDigitalSolution.Application.Features.CoachManager.Commands
{
    public class CreateCoachCommand : IRequest<CommandResult<CoachResponseDto>>
    {
        public CreateCoachDto Coach { get; init; } = default!;
    }
}