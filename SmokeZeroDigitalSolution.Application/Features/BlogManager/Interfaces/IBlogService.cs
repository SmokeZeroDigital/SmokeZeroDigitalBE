﻿using SmokeZeroDigitalSolution.Application.Features.BlogManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.BlogManager.Interfaces
{
    public interface IBlogService
    {
        Task<IQueryable<BlogArticle>> GetAllAsync();
        Task<BlogArticle> GetByIdAsync(Guid id);
        Task<BlogArticle> CreateAsync(CreateBlogDto dto);
        Task<BlogArticle> UpdateAsync(UpdateBlogDto dto);
        Task<IEnumerable<BlogArticle>> GetArticlesByTagAsync(string tag);
        Task<int> IncreaseViewCountAsync(Guid articleId);
    }
}
