namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class UserAchievement : BaseEntity
    {
        public Guid UserId { get; set; } // Foreign Key
        public AppUser User { get; set; } = default!; // Navigation Property

        public Guid AchievementId { get; set; } // Foreign Key
        public Achievement Achievement { get; set; } = default!; // Navigation Property

        public DateTime DateAchieved { get; set; }
    }
}
