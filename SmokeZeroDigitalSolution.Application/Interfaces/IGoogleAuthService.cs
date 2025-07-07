using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Interfaces
{
    public interface IGoogleAuthService
    {
        Task<string> VerifyGoogleTokenAsync(string idToken, CancellationToken cancellationToken = default);
    }
}
