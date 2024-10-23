using Microsoft.EntityFrameworkCore;
using IdentityServer.Infrastructure.Data;
using IdentityServer.Infrastructure.Repositories;
using IdentityServer.Domain.Repository;

namespace IdentityServer.API.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddCustomInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();

        return services;
    }
}
