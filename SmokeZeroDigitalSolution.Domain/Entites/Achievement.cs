using System.ComponentModel.DataAnnotations.Schema;

namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class Achievement : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public int ThresholdValue { get; set; }
        public string ThresholdType { get; set; } = string.Empty; // e.g., "DAYS_SMOKING_FREE", "MONEY_SAVED"

        public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
    }
}
