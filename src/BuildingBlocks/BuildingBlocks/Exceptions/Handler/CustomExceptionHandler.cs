using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public record Details(string Detail, string Title, int StatusCode);

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Error message: {exceptionMessage}, Time of ocurrence {time}", exception.Message, DateTime.UtcNow);

        Details details = exception switch
        {
            InternalServerException => new Details(exception.Message, exception.GetType().Name,
                StatusCodes.Status500InternalServerError),
            ValidationException => new Details(exception.Message, exception.GetType().Name,
                StatusCodes.Status400BadRequest),
            BadRequestException => new Details(exception.Message, exception.GetType().Name,
                StatusCodes.Status400BadRequest),
            NotFoundException => new Details(exception.Message, exception.GetType().Name,
                StatusCodes.Status404NotFound),
            _ => new Details(exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError),
        };

        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = context.Request.Path,
        };
        
        problemDetails.Extensions.Add("tracedId", context.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        }

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}