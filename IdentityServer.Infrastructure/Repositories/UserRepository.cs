using IdentityServer.Domain.Entities;
using IdentityServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Infrastructure.Repositories;

public class UserRepository(IdentityDbContext context)
{
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<bool> IsApplicationKeyValidAsync(string applicationKey)
    {
        return await context.Applications.AnyAsync(a => a.Key == applicationKey);
    }

    public async Task AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task AddUserToApplicationAsync(Guid userId, Guid applicationId)
    {
        var applicationUser = new ApplicationUser
        {
            UserId = userId,
            ApplicationId = applicationId
        };
        await context.ApplicationUsers.AddAsync(applicationUser);
        await context.SaveChangesAsync();
    }
}
