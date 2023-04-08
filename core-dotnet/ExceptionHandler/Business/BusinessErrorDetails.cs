using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core_dotnet.ExceptionHandler.Business;
public sealed class BusinessErrorDetails : ProblemDetails
{
    public object Errors { get; set; } = default!;
    public BusinessErrorDetails(string message)
    {
        Status = StatusCodes.Status400BadRequest;
        Type = "Business Exception";
        Title = "Business Error";
        Errors = message;
    }
}