using Microsoft.AspNetCore.Builder;

namespace core_dotnet.ExceptionHandler;

public static class ExceptionMiddlewareExtension
{
    public static void UseExceptionMiddleware(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMiddleware<ExceptionMiddleware>();
}