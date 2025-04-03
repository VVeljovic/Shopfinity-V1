using CoreLibrary.HandlingExceptions.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.HandlingExceptions
{
    public sealed class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (string Detail, string Title, int StatusCode) details = exception switch
            {
                InternalServerException =>
                (
                     exception.Message,
                     exception.GetType().Name,
                     httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                BadRequestException =>
                (
                     exception.Message,
                     exception.GetType().Name,
                     httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                NotFoundException =>
                (
                     exception.Message,
                     exception.GetType().Name,
                     httpContext.Response.StatusCode = StatusCodes.Status404NotFound
                ),
                _ =>
                (
                     exception.Message,
                     exception.GetType().Name,
                     httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = httpContext.Request.Path
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
