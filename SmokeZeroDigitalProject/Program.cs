
using SmokeZeroDigitalProject.Common.Converter;

namespace SmokeZeroDigitalProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
              options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
              options.JsonSerializerOptions.Converters.Add(new FlexibleDateTimeConverterFactory("yyyy-MM-ddTHH:mm:ss.fffZ", alwaysAssumeUtcOnRead: true));
                options.JsonSerializerOptions.Converters.Add(new FlexibleDateTimeConverterFactory("yyyy-MM-dd", alwaysAssumeUtcOnRead: true));
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.RegisterSwagger();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddExceptionHandler<CustomExceptionHandler>();
            builder.Services.AddScoped<IRequestExecutor, RequestExecutor>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<GlobalApiExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();
            app.MapControllers();

            app.Run();
        }
    }
}
