namespace SmokeZeroDigitalSolution.Contracts.User
{
    public class UpdateUserRequest
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
