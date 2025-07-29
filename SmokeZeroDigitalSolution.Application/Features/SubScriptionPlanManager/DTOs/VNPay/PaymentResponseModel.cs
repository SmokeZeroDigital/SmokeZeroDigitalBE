namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.VNPay
{
    public class PaymentResponseModel
    {
        public Guid UserId { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public string OrderDescription { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
