using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using StoreAPI.Application.DTOs;
using StoreAPI.Application.Exceptions;
using StoreAPI.Application.Interfaces;
using StoreAPI.Domain.Constants;

namespace StoreAPI.Application.Services;

public class UserService(
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    ITokenService tokenService,
    ILogger<UserService> logger) : IUserService
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly ILogger<UserService> _logger = logger;

    public async Task<bool> Signup(SignupDto model)
    {
        if (await _userManager.FindByNameAsync(model.Email) is not null)
        {
            throw new UserAlreadyExistsException($"User {model.Email} already exists.");
        }

        await EnsureRoleExists(Roles.User);

        var user = new IdentityUser
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Email,
            EmailConfirmed = true
        };

        var createUserResult = await _userManager.CreateAsync(user, model.Password);
        if (!createUserResult.Succeeded)
        {
            var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
            _logger.LogError("Failed to create user: {Errors}", errors);
            throw new Exception($"Failed to create user. Errors: {errors}");
        }

        var addUserToRoleResult = await _userManager.AddToRoleAsync(user, Roles.User);
        if (!addUserToRoleResult.Succeeded)
        {
            var errors = string.Join(", ", addUserToRoleResult.Errors.Select(e => e.Description));
            _logger.LogError("Failed to add role to user: {Errors}", errors);
        }

        return true;
    }

    public async Task<string> Login(LoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            throw new Exceptions.UnauthorizedAccessException("Incorrect credentials.");
        }

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        return _tokenService.GenerateAccessToken(authClaims);
    }

    private async Task EnsureRoleExists(string role)
    {
        if (!await _roleManager.RoleExistsAsync(role))
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(role));
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Failed to create role {Role}: {Errors}", role, errors);
                throw new Exception($"Failed to create role {role}. Errors: {errors}");
            }
        }
    }
}
