using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Common.IPersistence
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangeAsync();
    }
}
