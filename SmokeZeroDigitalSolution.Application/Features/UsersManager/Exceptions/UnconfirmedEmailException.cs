namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Exceptions;

public class UnconfirmedEmailException : Exception
{
    public Guid UserId { get; set; }
    public string? Email { get; set; }

    public UnconfirmedEmailException(string message) : base(message)
    {
    }

    public UnconfirmedEmailException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
