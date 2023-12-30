using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Services.Interface;

namespace Simbir.GO.Controllers;

[ApiController]
[Route("rent")]

public class RentController : BaseController
{
    private readonly IRentService _rentService;
    private readonly ITransportService _transportService;

    public RentController(IRentService rentService, ITransportService transportService)
    {
        _rentService = rentService;
        _transportService = transportService;
    }
    
    [AllowAnonymous]
    [HttpGet("transport")]
    public async Task<IActionResult> GetAvailableTransport(double latitude, double longitude, double radius, string type )
    {
        if (type != "Car" && type != "Bike" && type != "Scooter")
        {
            ModelState.AddModelError("", "Wrong data");
            return StatusCode(500, ModelState);
        }
        return Ok(await _transportService.GetByParamsAsync(latitude, longitude, radius, type));
    }
    
    [HttpGet("{rentId}")]
    public async Task<IActionResult> GetById([FromRoute] int rentId)
    {
        var rent = await _rentService.GetByIdAsync(rentId);
        var transport = await _transportService.GetByIdAsync(rent.TransportId);
        if (Id != rent.UserId && Id != transport.OwnerId){
            ModelState.AddModelError("", "You are not the owner of the rented transport or person who rented it");
            return StatusCode(500, ModelState);
        }
        return Ok(await _rentService.GetByIdAsync(rentId));
    }

    [HttpGet("myHistory")]
    public async Task<IActionResult> GetAllByCurrentUser()
    {
        return Ok(await _rentService.GetAllByUserIdAsync(Id));
    }
    
    [HttpGet("transportHistory/{transportId}")]
    public async Task<IActionResult> GetAllByTransport([FromRoute] int transportId)
    {
        var transport = await _transportService.GetByIdAsync(transportId);
        if (Id != transport.OwnerId){
            ModelState.AddModelError("", "You are not the owner of the transport");
            return StatusCode(500, ModelState);
        }
        return Ok(await _rentService.GetAllByTransportIdAsync(transportId));
    }
    
    [HttpPost("new/{transportId}")]
    public async Task<IActionResult> GetAllByTransport([FromRoute] int transportId, string rentType)
    {
        if (rentType != "Minutes" && rentType != "Days")
        {
            ModelState.AddModelError("", "Wrong data");
            return StatusCode(500, ModelState);
        }
        var transport = await _transportService.GetByIdAsync(transportId);
        if (Id == transport.OwnerId){
            ModelState.AddModelError("", "You cant rent your own transport");
            return StatusCode(500, ModelState);
        }
        return Ok(await _rentService.CreateWithRentTypeAsync(transportId, rentType, Id));
    }

    [HttpPost("end/{rentId}")]
    public async Task<IActionResult> FinishRentWithLocation([FromRoute] int rentId, double latitude, double longitude)
    {
        var rent = await _rentService.GetByIdAsync(rentId);
        if (Id != rent.UserId && !IsAdmin.GetValueOrDefault()){
            ModelState.AddModelError("", "Access Denied");
            return StatusCode(500, ModelState);
        }
        return Ok(await _rentService.FinishRentWithLocationAsync(rentId, latitude, longitude));
    }
}