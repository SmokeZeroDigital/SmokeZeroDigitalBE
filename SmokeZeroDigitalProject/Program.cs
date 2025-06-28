using eCommerce_BE.Infrastructure;
using MediatR;
using SmokeZeroDigitalProject.Common.Handlers;
using SmokeZeroDigitalProject.Common.Middlewares;
using SmokeZeroDigitalProject.Common.Swagger;
using SmokeZeroDigitalSolution.Application;

namespace SmokeZeroDigitalProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.RegisterSwagger();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddExceptionHandler<CustomExceptionHandler>();
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
