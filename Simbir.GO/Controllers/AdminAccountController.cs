using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.DTO;
using Simbir.GO.Services.Interface;

namespace Simbir.GO.Controllers;

[ApiController]
[Route("admin/account")]
[Authorize(Policy = "AdminOnly")]

public class AdminAccountController : BaseController
{
    private readonly IAccountService _accountService;

    public AdminAccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(int start, int count)
    {
        return Ok(await _accountService.GetAllLimitedAsync(start, count));
    }
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetById([FromRoute] int userId)
    {
        return Ok(await _accountService.GetByIdAsync(userId));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(AccountDto account)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await _accountService.CreateAccountAsync(account));
    }
    
    [HttpPut("{userId}")]
    public async Task<IActionResult> Update([FromRoute] int userId, AccountDto account)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await _accountService.UpdateAsync(account, userId));
    }
    
    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete([FromRoute] int userId)
    {
        return Ok(await _accountService.DeleteAsync(userId));
    }
}