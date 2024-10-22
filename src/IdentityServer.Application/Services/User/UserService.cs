using IdentityServer.Application.DTOs;
using IdentityServer.Infrastructure.Repositories.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Application.Services.User;

public class UserService(
    UserManager<Domain.Entities.User> userManager,
    UserRepository userRepository,
    ILogger<UserService> logger) : IUserService
{
    public async Task<bool> RegisterAsync(RegisterUserDto registerDto)
    {
        logger.LogInformation($"Registering new user: {registerDto.Username}");

        // Validate Application Key
        if (!await userRepository.IsApplicationKeyValidAsync(registerDto.ApplicationKey)) return false;

        // Create User
        var user = new Domain.Entities.User(registerDto.Username, registerDto.Email, registerDto.PhoneNumber);

        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            logger.LogError($"Failed to register user {registerDto.Username}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            return false;
        }

        logger.LogInformation($"User {registerDto.Username} registered successfully");

        // Associate User with Application
        var application = await userRepository.GetByUsernameAsync(registerDto.Username);
        await userRepository.AddUserToApplicationAsync(user.Id, application!.Id);

        return true;
    }
}
