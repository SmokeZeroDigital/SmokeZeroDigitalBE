using SmokeZeroDigitalSolution.Application.Features.CommentManager.Commands;
using SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.CommentManager.Queries;
using SmokeZeroDigitalSolution.Contracts.Comment;

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
            var request = new GetCommentByPostRequest { PostId = id };

            return await _executor.ExecuteQueryAsync<GetCommentByPostRequest, IEnumerable<CommentDto>>(
                request,
                req => new GetByPostQuery
                {
                    Id = req.PostId
                },
                nameof(GetCommentByPostId),
                cancellationToken);
        }

        [HttpGet("by-article/{id}")]
        public async Task<IActionResult> GetCommentByAriticleId([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetCommentByArticleRequest { ArrticleId = id };

            return await _executor.ExecuteQueryAsync<GetCommentByArticleRequest, IEnumerable<CommentDto>>(
                request,
                req => new GetByAriticleQuery
                {
                    Id = req.ArrticleId
                },
                nameof(GetCommentByPostId),
                cancellationToken);
        }

        [HttpGet("replies/{id}")]
        public async Task<IActionResult> GetReplies([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetRepliesRequest { ParentCommentId = id };

            return await _executor.ExecuteQueryAsync<GetRepliesRequest, IEnumerable<CommentDto>>(
                request,
                req => new GetRepliesQuery
                {
                    Id = req.ParentCommentId
                },
                nameof(GetCommentByPostId),
                cancellationToken);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteCommentRequest { Id = id };
            return await _executor.ExecuteAsync<DeleteCommentRequest, bool>(
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
