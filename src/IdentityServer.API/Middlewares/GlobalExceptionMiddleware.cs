﻿using IdentityServer.Domain.Models;
using System.Net;
using System.Text.Json;

namespace IdentityServer.API.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        ApiResponseModel<object> response;
        int statusCode;

        switch (exception)
        {
            case ArgumentException argEx:
                statusCode = (int)HttpStatusCode.BadRequest;
                response = new ApiResponseModel<object>
                {
                    Status = "error",
                    Message = argEx.Message,
                    StatusCode = statusCode
                };
                break;

            case UnauthorizedAccessException:
                statusCode = (int)HttpStatusCode.Unauthorized;
                response = new ApiResponseModel<object>
                {
                    Status = "error",
                    Message = "Unauthorized access.",
                    StatusCode = statusCode
                };
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                response = new ApiResponseModel<object>
                {
                    Status = "error",
                    Message = "An unexpected error occurred.",
                    StatusCode = statusCode
                };
                break;
        }

        context.Response.StatusCode = statusCode;
        var json = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(json);
    }
}
