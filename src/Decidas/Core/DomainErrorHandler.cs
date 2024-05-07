using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Decidas.Core;

public class DomainErrorHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancel)
    {
        if(exception is not DomainError)
        {
            return false;
        }

        httpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableContent;

        await httpContext.Response.WriteAsJsonAsync(exception.Message, cancel);

        return true;
    }
}
