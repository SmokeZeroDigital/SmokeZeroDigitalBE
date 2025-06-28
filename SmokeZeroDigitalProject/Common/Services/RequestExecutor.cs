namespace SmokeZeroDigitalProject.Common.Services
{
    public class RequestExecutor : IRequestExecutor
    {
        private readonly ISender _sender;

        public RequestExecutor(ISender sender)
        {
            _sender = sender;
        }

        public async Task<IActionResult> ExecuteAsync<TRequest, TResponse>(TRequest request, Func<TRequest, IRequest<CommandResult<TResponse>>> mapToRequest, string source, CancellationToken cancellationToken)
        {
            var mappedRequest = mapToRequest(request);
            var result = await _sender.Send(mappedRequest, cancellationToken);

            if (result.IsSuccess && result.Content is not null)
            {
                return new OkObjectResult(new ApiSuccessResult<TResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = $"Success executing {source}",
                    Content = result.Content
                });
            }

            var errorResult = new ApiErrorResult
            {
                Code = result.Unauthorized ? StatusCodes.Status401Unauthorized :
                       result.Forbidden ? StatusCodes.Status403Forbidden :
                       result.NotFound ? StatusCodes.Status404NotFound :
                       StatusCodes.Status400BadRequest,

                Message = result.Unauthorized ? "Unauthorized request." :
                          result.Forbidden ? "Forbidden request." :
                          result.NotFound ? "Resource not found." :
                          $"Failed executing {source}",

                Error = new Error(
                    innerException: string.Join("; ", result.Errors),
                    source: source,
                    stackTrace: null,
                    exceptionType: result.GetType().Name
                )
            };

            return errorResult.Code switch
            {
                StatusCodes.Status401Unauthorized => new UnauthorizedObjectResult(errorResult),
                StatusCodes.Status403Forbidden => new ObjectResult(errorResult) { StatusCode = StatusCodes.Status403Forbidden },
                StatusCodes.Status404NotFound => new NotFoundObjectResult(errorResult),
                _ => new BadRequestObjectResult(errorResult)
            };
        }

        public async Task<IActionResult> ExecuteQueryAsync<TRequest, TResponse>(TRequest request, Func<TRequest, IRequest<QueryResult<TResponse>>> mapToRequest, string source, CancellationToken cancellationToken)
        {
            var mappedRequest = mapToRequest(request);
            var result = await _sender.Send(mappedRequest, cancellationToken);

            if (result.IsSuccess && result.Content is not null)
            {
                return new OkObjectResult(new ApiSuccessResult<TResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = $"Success executing {source}",
                    Content = result.Content
                });
            }

            var errorResult = new ApiErrorResult
            {
                Code = result.Unauthorized ? StatusCodes.Status401Unauthorized :
                       result.Forbidden ? StatusCodes.Status403Forbidden :
                       result.NotFound ? StatusCodes.Status404NotFound :
                       StatusCodes.Status400BadRequest,

                Message = result.Unauthorized ? "Unauthorized request." :
                          result.Forbidden ? "Forbidden request." :
                          result.NotFound ? "Resource not found." :
                          $"Failed executing {source}",

                Error = new Error(
                    innerException: string.Join("; ", result.Errors),
                    source: source,
                    stackTrace: null,
                    exceptionType: result.GetType().Name
                )
            };

            return errorResult.Code switch
            {
                StatusCodes.Status401Unauthorized => new UnauthorizedObjectResult(errorResult),
                StatusCodes.Status403Forbidden => new ObjectResult(errorResult) { StatusCode = StatusCodes.Status403Forbidden },
                StatusCodes.Status404NotFound => new NotFoundObjectResult(errorResult),
                _ => new BadRequestObjectResult(errorResult)
            };
        }
    }
}
