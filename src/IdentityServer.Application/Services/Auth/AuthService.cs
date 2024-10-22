using IdentityServer.Application.DTOs;
using IdentityServer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityServer.Application.Services.Auth;

public class AuthService(
    UserManager<Domain.Entities.User> userManager, 
    UserRepository userRepository, 
    IConfiguration configuration, 
    ILogger<AuthService> logger) : IAuthService
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

    public async Task<string?> LoginAsync(LoginRequestDto loginDto)
    {
        logger.LogInformation($"User {loginDto.Username} attempting login");

        var user = await userRepository.GetByUsernameAsync(loginDto.Username);
        if (user == null || !await userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            logger.LogWarning($"Invalid login attempt for user {loginDto.Username}");
            return null;
        }

        logger.LogInformation($"User {loginDto.Username} logged in successfully");
        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(Domain.Entities.User user)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Name, user.UserName),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };

        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
