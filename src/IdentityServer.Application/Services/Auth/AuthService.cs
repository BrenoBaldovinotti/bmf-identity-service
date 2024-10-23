using IdentityServer.Application.DTOs;
using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityServer.Application.Services;

public class AuthService(
    UserManager<User> userManager, 
    IUserRepository userRepository, 
    IConfiguration configuration, 
    ILogger<AuthService> logger) : IAuthService
{
    private readonly string? _jwtSecretKey = configuration["Jwt:Key"];
    private readonly string? _jwtIssuer = configuration["Jwt:Issuer"];
    private readonly string? _jwtAudience = configuration["Jwt:Audience"];

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

    private string GenerateJwtToken(User user)
    {
        if (string.IsNullOrEmpty(_jwtSecretKey) || string.IsNullOrEmpty(_jwtIssuer) || string.IsNullOrEmpty(_jwtAudience))
        {
            throw new ArgumentNullException("JWT Settings", "JWT is not configured.");
        }

        if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UserName))
        {
            throw new ArgumentException("JWT Settings", "Invalid user email or username.");
        }

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Name, user.UserName),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };

        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _jwtIssuer,
            audience: _jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
