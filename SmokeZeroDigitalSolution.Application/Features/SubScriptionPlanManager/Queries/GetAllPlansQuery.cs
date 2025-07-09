using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Queries
{
    public class GetAllPlansQuery : IRequest<QueryResult<List<GetPlanResponseDto>>> { }
}
