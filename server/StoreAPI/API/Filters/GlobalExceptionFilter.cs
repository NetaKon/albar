using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreAPI.Application.Exceptions;

namespace StoreAPI.API.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var statusCode = context.Exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status400BadRequest,
            UserAlreadyExistsException => StatusCodes.Status409Conflict,
            Application.Exceptions.UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Result = new ObjectResult(new { error = context.Exception.Message })
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}
