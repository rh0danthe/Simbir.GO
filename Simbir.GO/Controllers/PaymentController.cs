using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Services.Interface;

namespace Simbir.GO.Controllers;

[ApiController]
[Route("payment")]
public class PaymentController : BaseController
{
    private readonly IAccountService _accountService;

    public PaymentController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpGet("{accountId}")]
    public async Task<IActionResult> AddMoney([FromRoute] int accountId)
    {
        if (Id != accountId && !IsAdmin.GetValueOrDefault())
        {
            ModelState.AddModelError("", "Access denied");
            return StatusCode(500, ModelState);
        }
        return Ok(await _accountService.AddMoneyAsync(accountId));
    }
}