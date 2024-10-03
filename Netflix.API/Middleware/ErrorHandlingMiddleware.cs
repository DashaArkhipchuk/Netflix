using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using Netflix.Application.Common.Errors;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Netflix.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {

            var problemDetails = GetDetails(ex);


            //var problemDetails = new ProblemDetails
            //{
            //    Type = "Internal Server Error",
            //    Title = "Error occured while processing your request",
            //    Status = (int)code,
            //    Detail = ex.Message,
            //};

            var result = JsonSerializer.Serialize(problemDetails);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
            return httpContext.Response.WriteAsync(result);

        }

        private static ProblemDetails GetDetails(Exception ex)
        {
            return ex switch
            {
                DuplicateEmailException exception => new ProblemDetails
                {
                    Type = "Duplicate Value Error",
                    Title = "Duplicate Value",
                    Status = (int)HttpStatusCode.Conflict,
                    Detail = exception.Message
                },

                AlreadyExistsException alreadyExists => new ProblemDetails
                {
                    Type = "Value already exists",
                    Title = "Already exists",
                    Status = (int)HttpStatusCode.Conflict,
                    Detail = alreadyExists.Message
                },

                NotFoundException => new ProblemDetails
                {
                    Type = "Not Found Error",
                    Title = "Not found",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = ex.Message
                },

                ValidationException validationEx => new ProblemDetails
                {
                    Type = "https://example.com/probs/validation",
                    Title = "Validation Error",
                    Status = (int)HttpStatusCode.BadRequest,
                    Detail = string.Join("; ", validationEx.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"))
                },

                ParsingValidationException parsingEx => new ProblemDetails
                {
                    Type = "https://example.com/probs/parsing",
                    Title = "Parsing Error",
                    Status = parsingEx.code,
                    Detail = ex.Message
                },

                _ => new ProblemDetails
                {
                    Type = "https://example.com/probs/generic",
                    Title = "An error occurred",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = ex.Message
                }
            };
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
