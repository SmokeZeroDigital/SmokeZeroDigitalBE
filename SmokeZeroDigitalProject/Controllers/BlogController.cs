using SmokeZeroDigitalSolution.Application.Features.BlogManager.Commands;
using SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.BlogManager.Queries;
using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Commands;
using SmokeZeroDigitalSolution.Contracts.Blog;
using SmokeZeroDigitalSolution.Contracts.Noti;
using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : BaseApiController
    {
        private readonly IRequestExecutor _executor;
        public BlogController(ISender sender, IRequestExecutor executor) : base(sender)
        {
            _executor = executor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<CreateBlogRequest, BlogArticle>(
                request,
                req => new CreateBlogCommand
                {
                    CreateBlogDto = new CreateBlogDto
                    {
                        Title = req.Title,
                        Content = req.Content,
                        AuthorUserId = req.AuthorUserId,
                        Tags = req.Tags
                    }
                },
                nameof(CreateBlog),
                cancellationToken);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBlog([FromBody] UpdateBlogRequest request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync<UpdateBlogRequest, BlogArticle>(
                request,
                req => new UpdateBlogCommand
                {
                    Dto = new UpdateBlogDto
                    {
                        Id = req.Id,
                        Title = req.Title,
                        Content = req.Content,
                        Tags = req.Tags
                    }
                },
                nameof(UpdateBlog),
                cancellationToken);
        }
        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetBlogByIdRequest { Id = id };

            return await _executor.ExecuteQueryAsync<GetBlogByIdRequest, BlogArticle>(
                request,
                req => new GetBlogByIdQuery
                {
                    BlogId = req.Id
                },
                nameof(GetById),
                cancellationToken);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlog(CancellationToken cancellationToken)
        {
            var request = new GetAllBlogRequest();

            return await _executor.ExecuteQueryAsync<GetAllBlogRequest, IQueryable<BlogReponseDto>>(
                request,
                req => new AllBlogQuery(),
                nameof(GetAllBlog),
                cancellationToken);
        }

        [HttpGet("by-tag/{tag}")]
        public async Task<IActionResult> GetByTag([FromRoute] String tag, CancellationToken cancellationToken)
        {
            var request = new GetBlogByTagRequest { Tag = tag };

            return await _executor.ExecuteQueryAsync<GetBlogByTagRequest, IEnumerable<BlogArticle>>(
                request,
                req => new GetBlogByTagQuery
                {
                    Tag = req.Tag
                },
                nameof(GetById),
                cancellationToken);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> IncreaseView([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new IncreaseViewBlogRequest { BlogId = id };
            return await _executor.ExecuteAsync<IncreaseViewBlogRequest, int>(
                request,
                req => new IncreaseViewCommand
                {
                   BlogId = req.BlogId
                },
                nameof(CreateBlog),
                cancellationToken);
        }
		[HttpDelete]
		public async Task<IActionResult> DeleteBlog([FromBody] DeleteBlogRequest request, CancellationToken cancellationToken)
		{
            Console.WriteLine($"DeleteBlogRequest: {request.Id}");
            return await _executor.ExecuteAsync<DeleteBlogRequest, bool>(
				request,
				req => new DeleteBlogCommand
				{
					Id = req.Id,
                    
				},
				nameof(DeleteBlog),
				cancellationToken);
		}

	}
}
