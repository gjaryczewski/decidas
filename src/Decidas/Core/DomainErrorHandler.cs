using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Decidas.Core;

public class DomainErrorHandler : IExceptionHandler
{
    private readonly ILogger<DomainErrorHandler> _logger;

    public DomainErrorHandler(ILogger<DomainErrorHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if(exception is not DomainError)
        {
            _logger.LogInformation("This is NOT a domain error.");
            return false;
        }

        _logger.LogInformation("This is a domain error.");
        httpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableContent;

        await httpContext.Response.WriteAsJsonAsync(exception.Message, cancellationToken);

        return true;
    }
}
