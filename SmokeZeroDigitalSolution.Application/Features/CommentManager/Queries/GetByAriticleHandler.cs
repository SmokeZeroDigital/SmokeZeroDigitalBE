using SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.CommentManager.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.Queries
{
    public class GetByAriticleHandler : IRequestHandler<GetByAriticleQuery, QueryResult<IEnumerable<CommentDto>>>
    {
        private readonly ICommentService _commentService;
        public GetByAriticleHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<QueryResult<IEnumerable<CommentDto>>> Handle(GetByAriticleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commentService.GetByArticleIdAsync(request.Id);
                return QueryResult<IEnumerable<CommentDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return QueryResult<IEnumerable<CommentDto>>.Failure(ex.Message);
            }
        }
    }
    
    
}
