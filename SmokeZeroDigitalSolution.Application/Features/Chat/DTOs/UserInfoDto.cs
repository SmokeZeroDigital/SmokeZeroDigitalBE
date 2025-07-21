using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.Chat.DTOs
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;

    }
}
