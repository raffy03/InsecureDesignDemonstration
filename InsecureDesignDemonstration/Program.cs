using InsecureDesignDemo.Models;
using Microsoft.EntityFrameworkCore;

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

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Profile}/{action=ViewProfile}/{id?}");

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/Home/Index");
                return Task.CompletedTask;
            });

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { Id = 1, Username = "Alice", Role = "User" },
                        new User { Id = 2, Username = "Bob", Role = "Admin" }
                    );
                    context.SaveChanges();
                }
            }

            app.Run();
        }
    }
}
