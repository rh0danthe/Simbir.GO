using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Common;

namespace Simbir.GO.Controllers;

[Authorize]
[ApiController]
public abstract class BaseController : ControllerBase
{
    private string AuthHeader => HttpContext.Request.Headers["Authorization"].ToString();

    protected int Id => Convert.ToInt32(Jwt.GetId(AuthHeader));
    protected string? Username => Jwt.GetUsername(AuthHeader);
    protected bool? IsAdmin => Jwt.GetIsAdmin(AuthHeader);
}