using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StoreAPI.Application.Interfaces;

namespace StoreAPI.Application.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly SymmetricSecurityKey _authSigningKey = new(
        Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Secret") ?? throw new InvalidOperationException("JWT Secret is missing."))
    );

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration.GetValue<string>("JWT:ValidIssuer"),
            Audience = _configuration.GetValue<string>("JWT:ValidAudience"),
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(_authSigningKey, SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
