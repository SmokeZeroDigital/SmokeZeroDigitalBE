using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager;

namespace SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager
{
    public interface ISecurityService
    {
        Task<LoginResultDto> LoginAsync(LoginRequest request);
        Task<bool> RegisterAsync(RegisterRequest request);
    }
}
