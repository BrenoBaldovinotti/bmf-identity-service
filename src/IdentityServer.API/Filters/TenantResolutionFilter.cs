using IdentityServer.Application.Utils;
using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Repository;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityServer.API.Filters;

public class TenantResolutionFilter(ITenantRepository tenantRepository) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!TryExtractApiKey(context, out var apiKey))
        {
            await RespondUnauthorized(context, "API Key is missing.");
            return;
        }

        if (string.IsNullOrEmpty(apiKey))
        {
            await RespondUnauthorized(context, "Invalid API Key.");
            return;
        }

        var tenant = await GetTenantByApiKey(apiKey!);
        if (tenant == null)
        {
            await RespondUnauthorized(context, "Invalid API Key.");
            return;
        }

        context.HttpContext.Items["Tenant"] = tenant;
        await next();
    }

    private static bool TryExtractApiKey(ActionExecutingContext context, out string apiKey)
    {
        if (context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey))
        {
            apiKey = extractedApiKey.ToString();
            return true;
        }
        else
        {
            apiKey = string.Empty;
            return false;
        }
    }

    private async Task<Tenant?> GetTenantByApiKey(string apiKey)
    {
        var hashedApiKey = ApiKeyGenerator.HashApiKey(apiKey);
        return await tenantRepository.GetByApiKeyHash(hashedApiKey);
    }

    private static async Task RespondUnauthorized(ActionExecutingContext context, string message)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.HttpContext.Response.WriteAsync(message);
    }
}