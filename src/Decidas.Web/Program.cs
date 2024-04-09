using Decidas.Web.Pages.Groups;
using System.Reflection;

namespace Decidas.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddSingleton<IDispatcher, Dispatcher>();

        var interfaceName = "ICommandHandler";
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var a in assemblies)
        {
            var types = a.GetExportedTypes();
            foreach (var t in types)
            {
                var interfaces = t.GetInterfaces();
                foreach (var i in interfaces)
                {
                    if (!i.IsGenericType && i.Name == interfaceName
                        || i.IsGenericType && i.Name.StartsWith($"{interfaceName}`"))
                    {
                        Console.WriteLine($"Interface name = {i.FullName}");
                        var assignbleTypes = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => i.IsAssignableFrom(p));
                        foreach (var at in assignbleTypes)
                        {
                            Console.WriteLine($"Assignable type = {at.FullName}");
                        }
                    }
                }
            }
        }

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
