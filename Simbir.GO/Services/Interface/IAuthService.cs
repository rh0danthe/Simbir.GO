using System.Security.Claims;
using Simbir.GO.DTO;

namespace Simbir.GO.Services.Interface;

public interface IAuthService
{
    string CreateToken(ICollection<Claim> claims, int tokenExpiresAfterHours = 0);
    Task<bool> CheckAccount(string  username);
}