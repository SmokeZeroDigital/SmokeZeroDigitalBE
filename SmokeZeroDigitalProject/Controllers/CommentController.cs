namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseApiController
    {
        private readonly IRequestExecutor _executor;
        public CommentController(ISender sender, IRequestExecutor executor) : base(sender)
        {
            _executor = executor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<CreateCommentRequest, CommentDto>(
                request,
                req => new CreateCommentCommand
                {
                    Comment = new CreateCommentDto
                    {
                        UserId = req.UserId,
                        Content = req.Content,
                        PostId = req.PostId,
                        ArticleId = req.ArticleId,
                        ParentCommentId = req.ParentCommentId
                    }
                },
                nameof(CreateComment),
                cancellationToken);
        }

        [HttpGet("by-post/{id}")]
        public async Task<IActionResult> GetCommentByPostId([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new RequestById { Id = id };

            return await _executor.ExecuteQueryAsync<RequestById, IEnumerable<CommentDto>>(
                request,
                req => new GetByPostQuery
                {
                    Id = req.Id
                },
                nameof(GetCommentByPostId),
                cancellationToken);
        }

        [HttpGet("by-article/{id}")]
        public async Task<IActionResult> GetCommentByAriticleId([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new RequestById { Id = id };

            return await _executor.ExecuteQueryAsync<RequestById, IEnumerable<CommentDto>>(
                request,
                req => new GetByAriticleQuery
                {
                    Id = req.Id
                },
                nameof(GetCommentByPostId),
                cancellationToken);
        }

        [HttpGet("replies/{id}")]
        public async Task<IActionResult> GetReplies([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new RequestById { Id = id };

            return await _executor.ExecuteQueryAsync<RequestById, IEnumerable<CommentDto>>(
                request,
                req => new GetRepliesQuery
                {
                    Id = req.Id
                },
                nameof(GetCommentByPostId),
                cancellationToken);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new RequestById { Id = id };
            return await _executor.ExecuteAsync<RequestById, bool>(
                request,
                req => new DeleteCommentCommand
                {
                    Id = req.Id
                },
                nameof(DeleteComment),
                cancellationToken);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<UpdateCommentRequest, CommentDto>(
                request,
                req => new UpdateCommentCommand
                {
                    Comment = new UpdateCommentDto
                    {
                        Id = req.Id,
                        Content = req.Content
                    }
                },
                nameof(UpdateComment),
                cancellationToken);
        }
    }
}
