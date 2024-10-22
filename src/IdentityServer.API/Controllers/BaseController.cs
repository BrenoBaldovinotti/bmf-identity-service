using IdentityServer.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API.Controllers;

[ApiController]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController() : ControllerBase
{
    protected IActionResult Success<T>(T data, string message = "Request processed successfully.")
    {
        return Ok(new ApiResponseModel<T>
        {
            Status = "success",
            Data = data,
            Message = message,
            StatusCode = StatusCodes.Status200OK
        });
    }

    protected IActionResult Success(string message = "Request processed successfully.")
    {
        return Ok(new ApiResponseModel<object>
        {
            Status = "success",
            Message = message,
            StatusCode = StatusCodes.Status200OK
        });
    }

    protected IActionResult Error(string message, Dictionary<string, string[]>? errors = null)
    {
        return BadRequest(new ApiResponseModel<object>
        {
            Status = "error",
            Message = message,
            Errors = errors,
            StatusCode = StatusCodes.Status400BadRequest
        });
    }

    protected IActionResult ValidationError(Dictionary<string, string[]> errors)
    {
        return BadRequest(new ApiResponseModel<object>
        {
            Status = "error",
            Message = "Validation failed.",
            Errors = errors,
            StatusCode = StatusCodes.Status400BadRequest
        });
    }
}