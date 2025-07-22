using Books.Application.Models;
using FluentValidation;
using Books.Contracts.Responses;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Books.API.Mapping;

public class ValidationMappingMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMappingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException e)
        {
            context.Response.StatusCode = 400;
            var validationFailureResponse = new ValidationFailureResponse
            {
                
                Errors = e.Errors.Select(x => new ValidationResponse
                {
                    PropertyName = x.PropertyName,
                    Message = x.ErrorMessage
                })
            };
            
            await context.Response.WriteAsJsonAsync(validationFailureResponse);
        }
    }
}