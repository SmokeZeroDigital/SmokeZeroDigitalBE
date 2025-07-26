using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth
{
    public class ConfirmEmailResultDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
