using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Simbir.GO.Common;
using Simbir.GO.Config;
using Simbir.GO.DTO;
using Simbir.GO.Interface;
using Simbir.GO.Services.Interface;

namespace Simbir.GO.Services;

public class AuthService : IAuthService
{
    private readonly IAccountRepository _accountRepository;
    
    public AuthService(IAccountRepository accountRepository)
    {
        this._accountRepository = accountRepository;
    }
    
    public string CreateToken(ICollection<Claim> claims, int tokenExpiresAfterHours = 0) 
    {
        var authSigningKey = AuthOptions.GetSymmetricSecurityKey();
        if (tokenExpiresAfterHours == 0)
        {
            tokenExpiresAfterHours = AuthOptions.TokenExpiresAfterHours;
        }

        var token = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            expires: DateTime.UtcNow.AddHours(tokenExpiresAfterHours),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
 
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<bool> CheckAccount(string username)
    {
        return await _accountRepository.CheckAccount( username);
    }

}