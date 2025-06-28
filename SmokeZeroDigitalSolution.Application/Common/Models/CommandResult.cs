namespace SmokeZeroDigitalSolution.Application.Common.Models;

public class CommandResult<T>
{
    public T? Content { get; set; }
    public bool IsSuccess { get; set; }
    public bool Unauthorized { get; set; }
    public bool Forbidden { get; set; }
    public bool NotFound { get; set; }

    public List<string> Errors { get; set; } = new();

    public static CommandResult<T> Success(T data) => new() { IsSuccess = true, Content = data };

    public static CommandResult<T> Failure(string message, List<string>? errors = null) =>
        new() { IsSuccess = false, Errors = errors ?? new() { message } };

    public static CommandResult<T> UnauthorizedResult(string message) =>
        new() { Unauthorized = true, Errors = new() { message } };

    public static CommandResult<T> ForbiddenResult(string message) =>
        new() { Forbidden = true, Errors = new() { message } };

    public static CommandResult<T> NotFoundResult(string message) =>
        new() { NotFound = true, Errors = new() { message } };
}
