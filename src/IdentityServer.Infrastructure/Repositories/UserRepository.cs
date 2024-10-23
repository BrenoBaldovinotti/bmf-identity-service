using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Repository;
using IdentityServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Infrastructure.Repositories;

public class UserRepository(IdentityDbContext context) : IUserRepository
{
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<bool> IsApplicationKeyValidAsync(string applicationKey)
    {
        return await context.Tenants.AnyAsync(a => a.ApiKeyHash == applicationKey);
    }

    public async Task AddAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task AddUserToApplicationAsync(Guid userId, Guid applicationId)
    {
        var applicationUser = new TenantUser
        {
            UserId = userId,
            TenantId = applicationId
        };
        await context.TenantUsers.AddAsync(applicationUser);
        await context.SaveChangesAsync();
    }
}
