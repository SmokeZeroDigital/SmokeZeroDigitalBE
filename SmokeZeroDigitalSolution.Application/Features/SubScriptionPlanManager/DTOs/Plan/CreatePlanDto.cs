using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.Plan
{
    public class CreatePlanDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; } 
        public bool IsActive { get; set; } = true; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime? UpdatedAt { get; set; } 
    }
}
