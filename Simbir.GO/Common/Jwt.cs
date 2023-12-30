using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Simbir.GO.Common;

public class Jwt
{
    public static string? GetId(string token)
    {
        return ParserToken(token, "accountId");
    }
    
    public static string? GetUsername(string token)
    {
        return ParserToken(token, "username");
    }
    
    public static bool? GetIsAdmin(string token)
    {
        return ParserToken(token, "admin") == "True";
    }

    private static string? ParserToken(string token, string role)
    {
        var removeBearer = token.Split(' ')[1];
        var handler = new JwtSecurityTokenHandler();
        var tokenData = handler.ReadJwtToken(removeBearer).Payload;
        return tokenData.Claims.FirstOrDefault(c => c.Type.Split('/').Last() == role)?.Value;
    }
    
    public static List<Claim> GetClaims(int id, string username, bool isAdmin)
    {
        var claims = new List<Claim>()
        {
            new Claim("accountId", id.ToString()),
            new Claim("username", username),
            new Claim("admin", isAdmin.ToString()),
        };

        return claims;
    }
}