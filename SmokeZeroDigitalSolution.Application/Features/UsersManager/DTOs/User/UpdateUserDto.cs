using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.User
{
    public class UpdateUserDto
    {
        public Guid? UserId { get; init; }
        public Guid? PlanId { get; init; }
        public string? Email { get; init; }
        public DateTime? DateOfBirth { get; init; }
        public string? FullName { get; init; }
        public bool EmailConfirmed { get; init; }
        public bool? IsDeleted { get; init; }
    }
}
