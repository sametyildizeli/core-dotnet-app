using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace core_dotnet.ExceptionHandler;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) { _next = next; }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = Application.Json;

        var response = ErrorResponseFactory.CreateErrorResponse(exception);

        context.Response.StatusCode = response.Status!.Value;

        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}