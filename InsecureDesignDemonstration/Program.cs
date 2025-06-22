using InsecureDesignDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InsecureDesignDemonstration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("AppDb"));

            builder.Services.AddAuthentication("DemoAuth")
                .AddCookie("DemoAuth", options =>
                {
                    options.LoginPath = "/home/index";
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                    new Claim(ClaimTypes.Name, "Alice"),
                    new Claim(ClaimTypes.Role, "User")
                };
                var identity = new ClaimsIdentity(claims, "DemoAuth");
                context.User = new ClaimsPrincipal(identity);
                await next.Invoke();
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/Home/Index");
                return Task.CompletedTask;
            });

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!db.Users.Any())
                {
                    db.Users.AddRange(
                        new User { Id = 1, Username = "Alice", Role = "User" },
                        new User { Id = 2, Username = "Bob", Role = "Admin" }
                    );
                    db.SaveChanges();
                }
            }

            app.Run();
        }
    }
}
