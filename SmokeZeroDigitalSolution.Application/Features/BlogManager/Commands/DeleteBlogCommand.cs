using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Commands
{
	public class DeleteBlogCommand : IRequest<CommandResult<bool>>
	{
		public Guid Id { get; init; } = default!;

	}
}
