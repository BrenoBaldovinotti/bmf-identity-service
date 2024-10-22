using IdentityServer.Application.DTOs;

namespace IdentityServer.Application.Services.User;

public interface IUserService
{
    Task<bool> RegisterAsync(RegisterUserDto registerDto);
}
