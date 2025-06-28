using SmokeZeroDigitalSolution.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Interfaces
{
    public interface IJWTService
    {
        Task<string> CreateTokenAsync(AppUser user);
    }
}
