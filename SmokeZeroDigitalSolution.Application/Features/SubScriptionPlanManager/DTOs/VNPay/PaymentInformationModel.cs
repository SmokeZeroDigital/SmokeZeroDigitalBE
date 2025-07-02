using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.VNPay
{
    public class PaymentInformationModel
    {
        public Guid UserId { get; set; }               
        public Guid SubscriptionPlanId { get; set; }   
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }
    }
}
