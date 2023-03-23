using core_dotnet.ExceptionHandler.Authorization;
using core_dotnet.ExceptionHandler.Business;
using core_dotnet.ExceptionHandler.Internal;
using core_dotnet.ExceptionHandler.Validation;
using Microsoft.AspNetCore.Mvc;

namespace core_dotnet.ExceptionHandler;

public static class ErrorResponseFactory
{
    public static ProblemDetails CreateErrorResponse(Exception exception)
    {
        ProblemDetails response = exception switch
        {
            AuthorizationException => new AuthorizationErrorDetails(exception.Message),
            BusinessException => new BusinessErrorDetails(exception.Message),
            ValidationException => new ValidationErrorDetails(exception),
            _ => new InternalErrorDetails(exception.Message)
        };

        return response;
    }
}