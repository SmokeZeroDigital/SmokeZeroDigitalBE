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

            //builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("Jwt"));

            builder.Services.RegisterChatRealTime(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
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

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("vi-VN"),
                SupportedCultures = new List<CultureInfo> { new CultureInfo("vi-VN") },
                SupportedUICultures = new List<CultureInfo> { new CultureInfo("vi-VN") }
            });

            app.MapRazorPages();
            app.MapControllers();
            app.MapHub<ChatHub>("/hubs/chat");
            app.Run();
        }
    }
}