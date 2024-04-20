#define FIRST // FIRST SECOND CC
#if NEVER
#elif FIRST
#region snippet1
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Decidas;

public class Program
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

        var app = builder.Build();

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
        #endregion
        #elif SECOND
        #region snippet2
        using Microsoft.AspNetCore.Authentication.Cookies;

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
        builder.Services.AddControllersWithViews();

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Forbidden/";
            });

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        var app = builder.Build();

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
        #endregion
        #elif CC
        #region snippet_cc
        using Microsoft.AspNetCore.Authentication.Cookies;

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
            .AddCookie(options =>
            {
                options.EventsType = typeof(CustomCookieAuthenticationEvents);
            });

        builder.Services.AddScoped<CustomCookieAuthenticationEvents>();

        var app = builder.Build();

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
        #endregion
        #endif
    }
}