using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Common.Services.SecurityManager
{
    public class LoginResultDto
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; } // Nếu trả về JWT Token
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public IList<string>? Errors { get; set; } // Danh sách lỗi nếu có
    }
}