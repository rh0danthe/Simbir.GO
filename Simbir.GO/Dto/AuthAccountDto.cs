using System.ComponentModel.DataAnnotations;

namespace Simbir.GO.DTO;

public class AuthAccountDto
{
    public string Username { get; set; }
    [MinLength(6)]
    public string Password { get; set; }
}