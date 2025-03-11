using System.Security.Claims;

namespace StoreAPI.Application.Interfaces;
public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
}