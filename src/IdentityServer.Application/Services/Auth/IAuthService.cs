using IdentityServer.Application.DTOs;

namespace IdentityServer.Application.Services;

public interface IAuthService
{
    Task<string?> LoginAsync(LoginRequestDto loginDto);
}
