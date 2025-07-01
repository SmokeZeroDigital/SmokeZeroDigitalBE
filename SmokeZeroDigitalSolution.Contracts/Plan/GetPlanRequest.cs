using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Contracts.Plan
{
    public class GetPlanRequest
    {
        public Guid Id { get; init; } = Guid.Empty;
    }
}
