using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs
{
    public class AuthResponseDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        // Thêm vào để trả lỗi khi thất bại
        public List<string> Errors { get; set; } = new();
        public string? Error { get; set; }  // dùng nếu chỉ có 1 error chính
    }

}