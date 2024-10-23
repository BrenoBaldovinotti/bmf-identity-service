using IdentityServer.Application.DTOs;

namespace IdentityServer.Application.Services;

public interface IUserService
{
    Task<bool> RegisterAsync(RegisterUserDto registerDto);
}
