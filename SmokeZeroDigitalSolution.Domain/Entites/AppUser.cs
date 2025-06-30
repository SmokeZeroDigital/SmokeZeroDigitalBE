namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public GenderType Gender { get; set; } = GenderType.Unknown;
        public string? ProfilePictureUrl { get; set; }
        public DateTime? RegistrationDate { get; set; } 

        // Các thuộc tính liên quan đến logic cai thuốc và gói thành viên
        public Guid? CurrentSubscriptionPlanId { get; set; }
        public SubscriptionPlan? CurrentSubscriptionPlan { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public decimal? CurrentMoneySaved { get; set; }
        public int? DaysSmokingFree { get; set; }
        public string? HealthImprovements { get; set; }
        public DateTime? CreatedAt { get; set; } 
        public DateTime? LastModifiedAt { get; set; }
        public bool? IsDeleted { get; set; } = false;
        // Navigation Properties cho các quan hệ 1-N
        public ICollection<QuittingPlan> QuittingPlans { get; set; } = new List<QuittingPlan>();
        public ICollection<SmokingRecord> SmokingRecords { get; set; } = new List<SmokingRecord>();
        public ICollection<ProgressEntry> ProgressEntries { get; set; } = new List<ProgressEntry>();
        public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public ICollection<BlogArticle> AuthoredArticles { get; set; } = new List<BlogArticle>(); // Nếu User có thể là tác giả bài blog
        public Coach? Coach { get; set; } // Navigation property nếu user là coach
        public ICollection<ChatMessage> SentMessages { get; set; } = new List<ChatMessage>();
    }
}
