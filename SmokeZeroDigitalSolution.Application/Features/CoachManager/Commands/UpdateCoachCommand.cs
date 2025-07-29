namespace SmokeZeroDigitalSolution.Application.Features.CoachManager.Commands
{
    public class UpdateCoachCommand : IRequest<CommandResult<CoachResponseDto>>
    {
        public Guid Id { get; set; }
        public UpdateCoachDto Coach { get; init; } = default!;
    }
}