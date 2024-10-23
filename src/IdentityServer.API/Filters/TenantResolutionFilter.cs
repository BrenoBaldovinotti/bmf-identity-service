using IdentityServer.Application.Utils;
using IdentityServer.Domain.Repository;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityServer.API.Filters;

public class TenantResolutionFilter(ITenantRepository tenantRepository) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey))
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.HttpContext.Response.WriteAsync("API Key is missing.");
            return;
        }

        if (string.IsNullOrEmpty(extractedApiKey))
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.HttpContext.Response.WriteAsync("Invalid API Key.");
            return;
        }

        var hashedApiKey = ApiKeyGenerator.HashApiKey(extractedApiKey);
        var tenant = await tenantRepository.GetByApiKeyHash(hashedApiKey);

        if (tenant == null)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.HttpContext.Response.WriteAsync("Invalid API Key.");
            return;
        }

        context.HttpContext.Items["Tenant"] = tenant;
        await next();
    }
}