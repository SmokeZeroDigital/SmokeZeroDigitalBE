using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, CommandResult<ConfirmEmailResultDto>>
{
    private readonly IAuthService _authService;

    public ConfirmEmailCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<CommandResult<ConfirmEmailResultDto>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var resultDto = await _authService.ConfirmEmailWithTokenAsync(request.ConfirmEmailDto.UserId, request.ConfirmEmailDto.Token);

        if (resultDto.Success)
        {
            return CommandResult<ConfirmEmailResultDto>.Success(resultDto);
        }

        return CommandResult<ConfirmEmailResultDto>.Failure(resultDto.Message);
    }
}
