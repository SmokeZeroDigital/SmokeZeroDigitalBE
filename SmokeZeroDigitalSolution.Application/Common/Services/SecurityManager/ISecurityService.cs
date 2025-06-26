using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager
{
    public interface ISecurityService
    {
        Task<LoginResultDto> RegisterUserAsync(RegisterRequest request);
        Task<LoginResultDto> LoginUserAsync(string email, string password);
    }
}
