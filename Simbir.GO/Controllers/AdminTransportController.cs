using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.DTO;
using Simbir.GO.Services.Interface;

namespace Simbir.GO.Controllers;

[ApiController]
[Route("admin/transport")]
[Authorize(Policy = "AdminOnly")]

public class AdminTransportController : BaseController
{
    private readonly ITransportService _transportService;

    public AdminTransportController(ITransportService transportService)
    {
        _transportService = transportService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(int start, int count, string transportType)
    {
        if (transportType != "Car" && transportType != "Bike" && transportType != "Scooter")
        {
            ModelState.AddModelError("", "Wrong data");
            return StatusCode(500, ModelState);
        }
        return Ok(await _transportService.GetAllLimitedAsync(start, count, transportType));
    }
    
    [HttpGet("{transportId}")]
    public async Task<IActionResult> GetById([FromRoute] int transportId)
    {
        return Ok(await _transportService.GetByIdAsync(transportId));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(AdminCreateTransportDto transport)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await _transportService.CreateAdminAsync(transport));
    }
    
    [HttpPut("{transportId}")]
    public async Task<IActionResult> Update([FromRoute] int transportId, UpdateTransportDto transport)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await _transportService.UpdateAsync(transportId, transport));
    }
    
    [HttpDelete("{transportId}")]
    public async Task<IActionResult> Delete([FromRoute] int transportId)
    {
        return Ok(await _transportService.DeleteAsync(transportId));
    }
}