using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Common.Behaviors
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull
    {
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            var response = await next();

            stopwatch.Stop();

            var requestName = typeof(TRequest).Name;
            var elapsed = stopwatch.ElapsedMilliseconds;

            if (elapsed > 500) // >500ms thì cảnh báo
            {
                _logger.LogWarning("⚠️ Long Running Request: {RequestName} ({ElapsedMilliseconds}ms)", requestName, elapsed);
            }
            else
            {
                _logger.LogInformation("⏱ Handled {RequestName} in {ElapsedMilliseconds}ms", requestName, elapsed);
            }

            return response;
        }
    }
}
