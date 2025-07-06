namespace SmokeZeroDigitalSolution.Contracts.Comment
{
    public class GetRepliesRequest
    {
        public Guid ParentCommentId { get; set; } // The ID of the parent comment to get replies for
    }
}
