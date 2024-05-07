using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace Decidas.Core;

public class DomainErrorHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancel)
    {
        if (exception is DomainError)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableContent;
        }

        return new ValueTask<bool>(false);
    }
}
