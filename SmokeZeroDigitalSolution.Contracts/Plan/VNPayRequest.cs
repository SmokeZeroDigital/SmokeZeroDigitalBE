namespace SmokeZeroDigitalSolution.Contracts.Plan
{
    public class VNPayRequest
    {
        public Guid SubscriptionPlanId { get; set; }
        public Guid UserId { get; set; }
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }
    }
}
