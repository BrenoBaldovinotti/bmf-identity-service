using IdentityServer.Application.DTOs;

namespace IdentityServer.Application.Services.User;

public interface IUserService
{
    Task<string> LoginAsync(LoginRequestDto request);
}
