using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.DTO;
using Simbir.GO.Services.Interface;

namespace Simbir.GO.Controllers;

[ApiController]
[Route("transport")]

public class TransportController : BaseController
{
    private readonly ITransportService _transportService;

    public TransportController(ITransportService transportService)
    {
        _transportService = transportService;
    }

    [AllowAnonymous]
    [HttpGet("{transportId}")]
    public async Task<IActionResult> GetById([FromRoute] int transportId)
    {
        return Ok(await _transportService.GetByIdAsync(transportId));
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserCreateTransportDto transport)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await _transportService.CreateUserAsync(Id, transport));
    }
    
    [HttpPut("{transportId}")]
    public async Task<IActionResult> Update([FromRoute] int transportId, UpdateTransportDto transport)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (Id != (await _transportService.GetByIdAsync(transportId)).OwnerId){
            ModelState.AddModelError("", "You are not the owner of this transport");
            return StatusCode(500, ModelState);
        }
        return Ok(await _transportService.UpdateAsync(transportId, transport));
    }
    
    [HttpDelete("{transportId}")]
    public async Task<IActionResult> DeleteCarAsync([FromRoute] int transportId)
    {
        if (Id != (await _transportService.GetByIdAsync(transportId)).OwnerId){
            ModelState.AddModelError("", "You are not the owner of this transport");
            return StatusCode(500, ModelState);
        }
        return Ok(await _transportService.DeleteAsync(transportId));
    }
}