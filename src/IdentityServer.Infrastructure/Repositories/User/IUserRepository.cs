namespace IdentityServer.Infrastructure.Repositories.User;

public interface IUserRepository
{
    Task<Domain.Entities.User?> GetByUsernameAsync(string username);
    Task<bool> IsApplicationKeyValidAsync(string applicationKey);
    Task AddUserAsync(Domain.Entities.User user);
    Task AddUserToApplicationAsync(Guid userId, Guid applicationId);
}
