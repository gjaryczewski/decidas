using Decidas.Testing;
using Decidas.Web;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Decidas.Web.IntegrationTests;

public class IndexPageTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestWebApplicationFactory<Program> _factory;

    public IndexPageTests(
        TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    [Theory]
    [InlineData("/")]
    [InlineData("/Index")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType!.ToString());
    }
}