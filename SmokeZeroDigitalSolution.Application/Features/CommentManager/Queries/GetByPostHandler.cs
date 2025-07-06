using SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.CommentManager.Interfaces;
namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Queries
{
    public class GetByPostHandler : IRequestHandler<GetByPostQuery, QueryResult<IEnumerable<CommentDto>>>
    {
        private readonly ICommentService _commentService;
        public GetByPostHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<QueryResult<IEnumerable<CommentDto>>> Handle(GetByPostQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commentService.GetByPostIdAsync(request.Id);
                return QueryResult<IEnumerable<CommentDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<IEnumerable<CommentDto>>.Failure(ex.Message);
            }
        }
    }
    
    
}
