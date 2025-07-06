﻿using System.ComponentModel.DataAnnotations;

namespace SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs
{
    public class CreateCommentDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; } = string.Empty;

        public Guid? PostId { get; set; }  // Either PostId or ArticleId must be provided
        public Guid? ArticleId { get; set; }
        public Guid? ParentCommentId { get; set; }  // Null if top-level comment
    }
}
