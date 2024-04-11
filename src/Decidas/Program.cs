using Decidas.Web.Pages.Groups;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<AppDbContext>(options
            => options.UseSqlServer("name=ConnectionStrings:AppDb"));

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
