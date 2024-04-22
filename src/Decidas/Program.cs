using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Decidas;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages(options =>
        {
            options.Conventions.AllowAnonymousToPage("/");
            options.Conventions.AuthorizeFolder("/Accounts");
            options.Conventions.AllowAnonymousToPage("/Accounts/Login");
            options.Conventions.AllowAnonymousToPage("/Accounts/Logout");
            options.Conventions.AllowAnonymousToPage("/Accounts/Register");
            options.Conventions.AuthorizeFolder("/Gatherings");
            options.Conventions.AuthorizeFolder("/Groups");
            options.Conventions.AuthorizeFolder("/Keepers");
            options.Conventions.AuthorizeFolder("/Members");
            options.Conventions.AuthorizeFolder("/Topics");
            options.Conventions.AllowAnonymousToFolder("/Contact");
        });
        
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<MainDbContext>(
            options => options.UseSqlServer("name=ConnectionStrings:MainDb"));

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

        builder.Services.AddHttpContextAccessor();

        builder.Services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var context = service.GetService<MainDbContext>();
                Data.DevelopmentInitialData.Publish(context);
            }
        }

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapDefaultControllerRoute();

        app.Run();
    }
}