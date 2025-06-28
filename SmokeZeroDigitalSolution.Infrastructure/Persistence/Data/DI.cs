namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Data
{
    public static class DI
    {
        public static IServiceCollection RegisterDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseNpgsql(configuration.GetConnectionString("MyDB")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
