using Decidas.Areas.Content;
using Decidas.Areas.Structure;
using Decidas.Areas.People;
using Decidas.Core;

namespace Decidas;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        builder.Services.AddRazorPages();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddProblemDetails();

        builder.Services.AddCoreServices(configuration);
        builder.Services.AddAreaContentServices();
        builder.Services.AddAreaStructureServices();
        builder.Services.AddAreaPeopleServices();

        builder.Services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
            options.AppendTrailingSlash = true;
        });

        var app = builder.Build();

        app.UseExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/Error");

            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapRazorPages();
        app.MapControllers();

        app.Run();
    }
}
