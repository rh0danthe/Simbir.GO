using Microsoft.AspNetCore.Mvc;
using Simbir.GO.DTO;
using Simbir.GO.Services.Interface;

namespace Simbir.GO.Controllers;

[ApiController]
[Route("account")]
public class AccountController: BaseController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        return Ok(new GetAccountResponse()
        {
            Id = Id,
            Username = Username,
            IsAdmin = IsAdmin.Value,
            Balance = (await _accountService.GetByIdAsync(Id)).Balance
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateMe(AuthAccountDto data)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var dbData = await _accountService.UpdateInfoAsync(data, Id);
        return Ok(dbData);
    }
}