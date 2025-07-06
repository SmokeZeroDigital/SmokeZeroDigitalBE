using SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.CommentManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Queries
{
    public class GetRepliesHandler : IRequestHandler<GetRepliesQuery, QueryResult<IEnumerable<CommentDto>>>
    {
        private readonly ICommentService _commentService;
        public GetRepliesHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<QueryResult<IEnumerable<CommentDto>>> Handle(GetRepliesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commentService.GetRepliesAsync(request.Id);
                return QueryResult<IEnumerable<CommentDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<IEnumerable<CommentDto>>.Failure(ex.Message);
            }
        }
    }
  
}
