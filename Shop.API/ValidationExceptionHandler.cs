using Microsoft.AspNetCore.Diagnostics;

namespace Shop.API;

internal sealed class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var domainException = Unwrap(exception);
        if (domainException is not ArgumentException argEx)
            return false;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(
            new { message = argEx.Message, parameter = argEx.ParamName },
            cancellationToken);

        return true;
    }

    private static Exception Unwrap(Exception ex)
    {
        var current = ex;
        while (current.InnerException is not null && current is not ArgumentException)
            current = current.InnerException;
        return current;
    }
}
