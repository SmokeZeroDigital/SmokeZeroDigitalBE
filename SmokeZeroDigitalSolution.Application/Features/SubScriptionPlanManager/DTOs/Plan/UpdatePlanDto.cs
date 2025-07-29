using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.Plan
{
    public class UpdatePlanDto
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true; // Default to true if not specified
    }
}
