namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Commands
{
    public class IncreaseViewCommand : IRequest<CommandResult<int>>
    {
        public Guid BlogId { get; set; }
    }

}
