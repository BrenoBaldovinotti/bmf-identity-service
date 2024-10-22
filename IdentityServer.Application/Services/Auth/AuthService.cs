using IdentityServer.Application.DTOs;
using IdentityServer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityServer.Application.Services.Auth;

public class AuthService(UserManager<Domain.Entities.User> userManager, UserRepository userRepository, IConfiguration configuration) : IAuthService
{
    public async Task<bool> RegisterAsync(RegisterUserDto registerDto)
    {
        // Validate Application Key
        if (!await userRepository.IsApplicationKeyValidAsync(registerDto.ApplicationKey)) return false;

        // Create User
        var user = new Domain.Entities.User
        {
            UserName = registerDto.Username,
            Email = registerDto.Email
        };

        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) return false;

        // Associate User with Application
        var application = await userRepository.GetByUsernameAsync(registerDto.Username);
        await userRepository.AddUserToApplicationAsync(user.Id, application!.Id);

        return true;
    }

    public async Task<string?> LoginAsync(LoginRequestDto loginDto)
    {
        var user = await userRepository.GetByUsernameAsync(loginDto.Username);
        if (user == null || !await userManager.CheckPasswordAsync(user, loginDto.Password))
            return null;

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(Domain.Entities.User user)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
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
