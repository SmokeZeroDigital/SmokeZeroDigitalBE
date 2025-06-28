namespace SmokeZeroDigitalProject.Common.Interfaces
{
    public interface IRequestExecutor
    {
        Task<IActionResult> ExecuteAsync<TRequest, TResponse>(
       TRequest request,
       Func<TRequest, IRequest<CommandResult<TResponse>>> mapToRequest,
       string source,
       CancellationToken cancellationToken);

        Task<IActionResult> ExecuteQueryAsync<TRequest, TResponse>(
            TRequest request,
            Func<TRequest, IRequest<QueryResult<TResponse>>> mapToRequest,
            string source,
            CancellationToken cancellationToken);
    }
}
