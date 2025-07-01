using Microsoft.AspNetCore.Http;

namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Commands
{
    public class VNPayCommandHandler : IRequestHandler<VNPayCommand, CommandResult<PaymentResponseModel>>
    {
        private readonly IVNPayService _vnPayService;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public VNPayCommandHandler(IVNPayService vnPayService, IUnitOfWork unitOfWork, IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _vnPayService = vnPayService;
            _authService = authService;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CommandResult<PaymentResponseModel>> Handle(VNPayCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;
                var result = _vnPayService.CreatePaymentUrl(request.Payment, context);
                await _unitOfWork.SaveAsync(cancellationToken);
                return CommandResult<PaymentResponseModel>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<PaymentResponseModel>.Failure(ex.Message);
            }
        }
    }
}
