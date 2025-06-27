namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets cho các Entities của bạn
        public DbSet<Achievement> Achievements { get; set; } = default!;
        public DbSet<BlogArticle> BlogArticles { get; set; } = default!;
        public DbSet<ChatMessage> ChatMessages { get; set; } = default!;
        public DbSet<Coach> Coaches { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public DbSet<Feedback> Feedbacks { get; set; } = default!;
        public DbSet<Notification> Notifications { get; set; } = default!;
        public DbSet<Post> Posts { get; set; } = default!;
        public DbSet<ProgressEntry> ProgressEntries { get; set; } = default!;
        public DbSet<QuittingPlan> QuittingPlans { get; set; } = default!;
        public DbSet<SmokingRecord> SmokingRecords { get; set; } = default!;
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; } = default!;
        public DbSet<UserAchievement> UserAchievements { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            var roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Member", NormalizedName = "MEMBER" },
                new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Coach", NormalizedName = "COACH" },
                new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" },
            };
            builder.Entity<IdentityRole<Guid>>().HasData(roles);
            builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
            base.OnModelCreating(builder);

        }
    }
}