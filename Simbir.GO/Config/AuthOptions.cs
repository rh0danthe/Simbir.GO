using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Simbir.GO.Config;

public class AuthOptions
{
    public const string Issuer = "MyAuthServer"; // издатель токена
    public const string Audience = "MyAuthClient"; // потребитель токена
    const string Key = "wtuNRpAZ3n7iWgWGKPeSEgQUTHGFOOEe-YS4PxHFnir7F881"; // ключ для шифрации
    public const int TokenExpiresAfterHours = 72; // время жизни лол

    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
}