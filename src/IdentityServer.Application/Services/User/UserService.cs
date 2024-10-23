using IdentityServer.Application.DTOs;
using IdentityServer.Application.Utils;
using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Application.Services;

public class UserService(
    UserManager<User> userManager,
    IUserRepository userRepository,
    ILogger<UserService> logger) : IUserService
{
    public async Task<bool> RegisterAsync(RegisterUserDto registerDto)
    {
        logger.LogInformation($"Registering new user: {registerDto.Username}");

        if (!await userRepository.IsApplicationKeyValidAsync(registerDto.ApplicationKey)) return false;

        var (passwordHash, salt) = PasswordHelper.HashPassword(registerDto.Password);
        var user = new User(
            registerDto.Username, 
            registerDto.Email, 
            passwordHash, 
            salt,
            registerDto.PhoneNumber
        );

        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            logger.LogError($"Failed to register user {registerDto.Username}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            return false;
        }

        logger.LogInformation($"User {registerDto.Username} registered successfully");

        var application = await userRepository.GetByUsernameAsync(registerDto.Username);
        await userRepository.AddUserToApplicationAsync(user.Id, application!.Id);

        return true;
    }
}
