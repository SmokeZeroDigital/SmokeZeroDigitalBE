using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class Token : BaseEntity
    {
        public Guid UserId { get; set; }  
        public string RefreshToken { get; set; } = string.Empty;
        public DateTimeOffset ExpiryDate { get; set; }

        // Optional for tracking
        public string? Device { get; set; }
        public string? IPAddress { get; set; }
        public bool IsRevoked { get; set; } = false;

        public AppUser User { get; set; } // Navigation property
    }
}
