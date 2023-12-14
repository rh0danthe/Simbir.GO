using Microsoft.AspNetCore.Mvc;
using Simbir.GO.DTO;

namespace Simbir.GO.Controllers;

[ApiController]
[Route("account")]
public class AccountController: BaseController
{
    [HttpGet("me")]
    public IActionResult GetMe()
    {
        return Ok(new AccountDto()
        {
            Username = Username,
            IsAdmin = IsAdmin.Value
        });
    }
}