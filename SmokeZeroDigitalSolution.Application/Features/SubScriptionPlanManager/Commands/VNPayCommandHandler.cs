using Microsoft.AspNetCore.Http;
using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.VNPay;
using SmokeZeroDigitalSolution.Application.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Commands
{
    public class VNPayCommandHandler : IRequestHandler<VNPayCommand, CommandResult<PaymentResponseModel>>
    {
        private readonly IVNPayService _vnPayService;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        public VNPayCommandHandler(IVNPayService vnPayService, IUnitOfWork unitOfWork, IAuthService authService)
        {
            _vnPayService = vnPayService;
            _authService = authService;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandResult<PaymentResponseModel>> Handle(VNPayCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _vnPayService.CreatePaymentUrl(request.Payment, HttpContext context);
                await _unitOfWork.SaveAsync(cancellationToken);
                return CommandResult<CreatePlanResultDto>.Success(result);
            }
            catch (Exception ex)
            {
                return CommandResult<PaymentResponseModel>.Failure(ex.Message);
            }
        }
    }
}
