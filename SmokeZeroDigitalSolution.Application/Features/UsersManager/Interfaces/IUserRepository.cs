using SmokeZeroDigitalSolution.Application.Common.IPersistence;
using SmokeZeroDigitalSolution.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Interfaces
{
    public interface IUserRepository : IBaseRepository<AppUser, Guid>
    {

    }
}
