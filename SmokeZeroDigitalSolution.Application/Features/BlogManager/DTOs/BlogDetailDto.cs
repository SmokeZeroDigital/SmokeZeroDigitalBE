namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs
{
    public class BlogDetailDto : BlogReponseDto
    {
        public Guid? AuthorUserId { get; set; }
        public List<CommentDto>? Comments { get; set; }
    }
}
