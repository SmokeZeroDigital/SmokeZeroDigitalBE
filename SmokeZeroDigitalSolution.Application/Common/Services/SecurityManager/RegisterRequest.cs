using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public int Gender { get; set; }
    }
}