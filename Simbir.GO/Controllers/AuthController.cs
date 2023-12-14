using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Common;
using Simbir.GO.DTO;
using Simbir.GO.Entities;
using Simbir.GO.Services.Interface;

namespace Simbir.GO.Controllers;

[Route("auth")]
public class AuthController : Controller
{
     private readonly IAuthService _authService;
    private readonly IAccountService _accountService;
    
    public AuthController(IAuthService authService, IAccountService accountService)
    {
        this._authService = authService;
        this._accountService = accountService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthAccountDto req)
    {
        var account = await _accountService.GetAccountData(req.Username, req.Password);
        
        if (account == null)
            return BadRequest("Bad credentials");

        var claims = Jwt.GetClaims(account.Id, req.Username, account.IsAdmin);
        var accessToken = _authService.CreateToken(claims, 1);
        
        return Ok(new AuthDtoResponse()
        {
            AccessToken = accessToken
        });
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthAccountDto req)
    {
        var refreshToken = _authService.CreateToken(new List<Claim>(), 72);

        if (await _authService.CheckAccount(req.Username))
            return BadRequest("email is already taken");

        var candidate = await _accountService.CreateAccount(new Account()
        {
            Password = req.Password,
            IsAdmin = false,
            Username = req.Username
        });

        var claims = Jwt.GetClaims(candidate.Id, candidate.Username, candidate.IsAdmin);
        var accessToken = _authService.CreateToken(claims, 1);
        
        return Ok(new AuthDtoResponse()
        {
            AccessToken = accessToken
        });
    }

}