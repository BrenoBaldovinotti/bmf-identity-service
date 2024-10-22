using IdentityServer.Application.DTOs;

namespace IdentityServer.Application.Services.Auth;

public interface IAuthService
{
    Task<string?> LoginAsync(LoginRequestDto loginDto);
}
