namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Commands
{
    public class VNPayCommand : IRequest<CommandResult<PaymentResponseModel>>
    {
        public PaymentInformationModel Payment { get; init; } = default!;
        public HttpContext HttpContext { get; init; }
    }
}
