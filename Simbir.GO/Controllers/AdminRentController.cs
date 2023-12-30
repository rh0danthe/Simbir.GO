using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.DTO;
using Simbir.GO.Services.Interface;

namespace Simbir.GO.Controllers;

[ApiController]
[Route("admin/rent")]
[Authorize(Policy = "AdminOnly")]
public class AdminRentController : BaseController
{
    private readonly IRentService _rentService;

    public AdminRentController(IRentService rentService)
    {
        _rentService = rentService;
    }
    
    [HttpGet("userHistory/{userId}")]
    public async Task<IActionResult> GetAllByUser([FromRoute] int userId)
    {
        return Ok(await _rentService.GetAllByUserIdAsync(userId));
    }
    
    [HttpGet("transportHistory/{transportId}")]
    public async Task<IActionResult> GetAllByTransport([FromRoute] int transportId)
    {
        return Ok(await _rentService.GetAllByTransportIdAsync(transportId));
    }
    
    [HttpGet("{rentId}")]
    public async Task<IActionResult> GetById([FromRoute] int rentId)
    {
        return Ok(await _rentService.GetByIdAsync(rentId));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(RentDto rent)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await _rentService.CreateAsync(rent));
    }
    
    [HttpPut("{rentId}")]
    public async Task<IActionResult> Update([FromRoute] int rentId, RentDto rent)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await _rentService.UpdateAsync(rentId, rent));
    }
    
    [HttpDelete("{rentId}")]
    public async Task<IActionResult> Delete([FromRoute] int rentId)
    {
        return Ok(await _rentService.DeleteAsync(rentId));
    }
}