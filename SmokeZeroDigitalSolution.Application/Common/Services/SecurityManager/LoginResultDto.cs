using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager
{
    public class LoginResultDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Errors { get; set; }
    }
}