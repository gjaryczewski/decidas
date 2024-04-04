using System.Data.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Decidas.Web;
using Microsoft.AspNetCore.Hosting;

namespace Decidas.Testing;

public class TestWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
    }
}