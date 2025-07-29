namespace SmokeZeroDigitalSolution.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //>>> AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //>>> FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //>>> MediatR
            services.AddMediatR(x =>
            {
                x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                x.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
                x.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                x.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            });

            // Ensure the required namespace is included for AddHttpContextAccessor
            services.AddHttpContextAccessor();

            //>>> Register services in Application.Features 
            var assembly = Assembly.GetExecutingAssembly();
            var featureTypes = assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract)
                .Where(type => type.Namespace != null && type.Namespace.StartsWith("Application.Features"));

            foreach (var type in featureTypes)
            {
                var interfaces = type.GetInterfaces();
                foreach (var serviceInterface in interfaces)
                {
                    services.AddScoped(serviceInterface, type);
                }
                if (!interfaces.Any())
                {
                    services.AddScoped(type);
                }
            }
            return services;
        }
    }
}
