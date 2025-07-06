namespace SmokeZeroDigitalSolution.Contracts.Comment
{
    public class RequestById
    {
        public Guid Id { get; set; } // The ID of the parent comment to get replies for
    }
}
