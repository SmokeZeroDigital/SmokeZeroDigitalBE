using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmokeZeroDigitalProject.Common.Models
{
    public class ApiErrorResult
    {
        public int? Code { get; init; }
        public string? Message { get; init; }
        public Error? Error { get; init; }
    }
}
