using SmokeZeroDigitalSolution.Domain.Enums;

namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public GenderType Gender { get; set; } = GenderType.Unknown;
        public string? ProfilePictureUrl { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        // Các thuộc tính liên quan đến logic cai thuốc và gói thành viên
        public Guid? CurrentSubscriptionPlanId { get; set; }
        public SubscriptionPlan? CurrentSubscriptionPlan { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }

        public decimal CurrentMoneySaved { get; set; }
        public int DaysSmokingFree { get; set; }
        public string? HealthImprovements { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedAt { get; set; }
        // Navigation Properties cho các quan hệ 1-N
        public ICollection<QuittingPlan> QuittingPlans { get; set; } = new List<QuittingPlan>();
        public ICollection<SmokingRecord> SmokingRecords { get; set; } = new List<SmokingRecord>();
        public ICollection<ProgressEntry> ProgressEntries { get; set; } = new List<ProgressEntry>();
        public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<ChatMessage> SentMessages { get; set; } = new List<ChatMessage>();
        public ICollection<ChatMessage> ReceivedMessages { get; set; } = new List<ChatMessage>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public ICollection<BlogArticle> AuthoredArticles { get; set; } = new List<BlogArticle>(); // Nếu User có thể là tác giả bài blog
        public ICollection<Coach> Coaches { get; set; } = new List<Coach>(); // Nếu một User có thể là Coach
    }
}
