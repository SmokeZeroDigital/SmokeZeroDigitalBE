namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public class CommentRepository(ApplicationDbContext applicationDbContext) : BaseRepository<Comment, Guid>(applicationDbContext), ICommentRepository
    {

        // Custom Methods
        public async Task<IEnumerable<Comment>> GetByPostIdAsync(Guid postId)
        {
            return await Get(c => c.PostId == postId && !c.IsDeleted && c.ParentCommentId == null)
                        .OrderByDescending(c => c.CommentDate)
                        .Include(c => c.User)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetByArticleIdAsync(Guid articleId)
        {
            return await Get(c => c.ArticleId == articleId && !c.IsDeleted && c.ParentCommentId == null)
                        .OrderByDescending(c => c.CommentDate)
                        .Include(c => c.User)
                        .Include(c => c.Replies)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetRepliesAsync(Guid parentCommentId)
        {
            return await Get(c => c.ParentCommentId == parentCommentId && !c.IsDeleted)
                        .OrderByDescending(c => c.CommentDate)
                        .Include(c => c.User)
                        .ToListAsync();
        }

        public async Task<CommentDto> CreateCommentAsync(CreateCommentDto dto)
        {
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                Content = dto.Content,
                PostId = dto.PostId,
                ArticleId = dto.ArticleId,
                ParentCommentId = dto.ParentCommentId,
                CommentDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            try
            {
                await AddAsync(comment);
                Console.WriteLine($"Comment created with ID: {comment.Id}");

                return new CommentDto
                {
                    Id = comment.Id,
                    UserId = comment.UserId,
                    PostId = comment.PostId,
                    ArticleId = comment.ArticleId,
                    Content = comment.Content,
                    CommentDate = comment.CommentDate,
                    ParentCommentId = comment.ParentCommentId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating comment", ex);


            }
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var comment = await FindAsync(id);
            if (comment == null)
            {
                throw new KeyNotFoundException("Comment not found.");
            }
            comment.IsDeleted = true;
            Update(comment);
            return true;
        }

        public async Task<CommentDto> UpdateCommentAsync(UpdateCommentDto data)
        {
            var comment = await FindAsync(data.Id);
            if (comment == null || comment.IsDeleted)
            {
                throw new KeyNotFoundException("Comment not found.");
            }
            comment.Content = data.Content;
            comment.LastModifiedAt = DateTime.UtcNow;
            Update(comment);
            return new CommentDto
            {
                Id = comment.Id,
                UserId = comment.UserId,
                PostId = comment.PostId,
                ArticleId = comment.ArticleId,
                Content = comment.Content,
                CommentDate = comment.LastModifiedAt ?? comment.CommentDate,
                ParentCommentId = comment.ParentCommentId
            };
        }
    }
    
}
