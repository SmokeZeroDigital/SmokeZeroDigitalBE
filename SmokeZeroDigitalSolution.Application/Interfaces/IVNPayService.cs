using Microsoft.AspNetCore.Http;
using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.VNPay;

namespace SmokeZeroDigitalSolution.Application.Interfaces
{
    public interface IVNPayService
    {
        /// <summary>
        /// Generates a VNPay payment URL based on the provided payment information and HTTP context.
        /// </summary>
        /// <param name="model">The payment information used to construct the VNPay request.</param>
        /// <param name="context">The HTTP context containing client request details.</param>
        /// <returns>A signed VNPay payment URL for redirecting the user to complete the transaction.</returns>
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        /// <summary>
        /// Processes a VNPay payment response from the provided query parameters.
        /// </summary>
        /// <param name="collections">The query parameters received from VNPay after payment completion.</param>
        /// <returns>A model containing the parsed and validated payment response data.</returns>
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
        /// <summary>
        /// Processes VNPay Instant Payment Notification (IPN) data from the provided query collection and returns the result.
        /// </summary>
        /// <param name="collections">The query parameters received from the VNPay IPN callback.</param>
        /// <returns>An <see cref="ErrorViewModel"/> representing the outcome of the IPN processing.</returns>
        ErrorViewModel PaymentExecuteIpn(IQueryCollection collections);
    }
}
