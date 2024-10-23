using IdentityServer.Application.Utils;
using IdentityServer.Domain.Repository;
using IdentityServer.Infrastructure.Data;

namespace IdentityServer.API.Middlewares;

public class ApiKeyMiddleware(RequestDelegate next, ITenantRepository tenantRepository)
{
    private const string ApiKeyHeaderName = "X-Api-Key";

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API Key is missing.");
            return;
        }

        if (string.IsNullOrEmpty(extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid API Key.");
            return;
        }

        var hashedApiKey = ApiKeyGenerator.HashApiKey(extractedApiKey);

        var tenant = await tenantRepository.GetByApiKeyHash(hashedApiKey);

        if (tenant == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid API Key.");
            return;
        }

        context.Items["Tenant"] = tenant;

        await next(context);
    }
}
