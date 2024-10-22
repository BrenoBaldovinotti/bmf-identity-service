using IdentityServer.Application.DTOs;

namespace IdentityServer.Application.Services.Auth;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterUserDto registerDto);
    Task<string?> LoginAsync(LoginRequestDto loginDto);
}
