public class VNPayCallbackCommandHandler : IRequestHandler<VNPayCallbackCommand, CommandResult<VNPayCallbackResultDto>>
{
    private readonly IVNPayService _vnPayService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;

    public VNPayCallbackCommandHandler(
        IVNPayService vnPayService,
        IUnitOfWork unitOfWork, IAuthService authService)
    {
        _vnPayService = vnPayService;
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task<CommandResult<VNPayCallbackResultDto>> Handle(VNPayCallbackCommand request, CancellationToken cancellationToken)
    {
        var response = _vnPayService.PaymentExecute(request.Query);

        var isSuccess = request.Query["vnp_ResponseCode"] == "00";
        var amount = double.TryParse(request.Query["vnp_Amount"], out var amt) ? amt / 100 : 0;

        // Extract UserId and SubscriptionPlanId from vnp_OrderInfo
        var orderInfo = request.Query["vnp_OrderInfo"].ToString();
        var parts = orderInfo.Split('|');
        Guid userId = Guid.Empty;
        Guid subscriptionPlanId = Guid.Empty;
        if (parts.Length >= 2)
        {
            Guid.TryParse(parts[0], out userId);
            Guid.TryParse(parts[1], out subscriptionPlanId);
        }

        if (isSuccess && userId != Guid.Empty && subscriptionPlanId != Guid.Empty)
        {
            if (isSuccess && userId != Guid.Empty && subscriptionPlanId != Guid.Empty)
            {
                var updateUserDto = new UpdateUserDto
                {
                    UserId = userId,
                    PlanId = subscriptionPlanId
                };
                await _authService.UpdateUserAsync(updateUserDto, cancellationToken);
                await _unitOfWork.SaveAsync(cancellationToken);
            }
        }

        return CommandResult<VNPayCallbackResultDto>.Success(new VNPayCallbackResultDto
        {
            IsSuccess = isSuccess,
            Amount = amount
        });
    }
}
