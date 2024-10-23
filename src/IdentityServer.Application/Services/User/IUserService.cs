using IdentityServer.Application.DTOs;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Application.Services;

public interface IUserService
{
    Task<bool> RegisterAsync(RegisterUserDto registerDto, Tenant tenant);
}
