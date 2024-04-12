using Decidas.Web.Pages.Groups;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        builder.Services.Configure<RazorViewEngineOptions>(options =>
        {
            options.AreaViewLocationFormats.Clear();
            options.AreaViewLocationFormats.Add("/Domain/{2}/Pages/{1}/{0}.cshtml");
            options.AreaViewLocationFormats.Add("/Domain/{2}/Pages/Shared/{0}.cshtml");
            options.AreaViewLocationFormats.Add("/Pages/Shared/{0}.cshtml");
        });

        builder.Services.AddDbContext<Database>(options
            => options.UseSqlServer("name=ConnectionStrings:Main"));

        builder.Services.AddTransient<ICommandDispatcher, CommandDispatcher>();
        builder.Services.AddTransient<ICommandHandler<CreateGroupCommand>, CreateGroupCommandHandler>();
        builder.Services.AddTransient<IGroupRepository, GroupRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}
