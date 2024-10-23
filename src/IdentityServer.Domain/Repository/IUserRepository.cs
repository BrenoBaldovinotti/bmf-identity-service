using IdentityServer.Domain.Entities;

namespace IdentityServer.Domain.Repository;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> IsApplicationKeyValidAsync(string applicationKey);
    Task AddUserAsync(User user);
    Task AddUserToApplicationAsync(Guid userId, Guid applicationId);
}
