using EventManager.LocationService;
using EventManager.Model;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Controllers;
[ApiController]
public class LocationsController : ControllerBase
{
    public readonly ILocationService _locationService;
    public readonly ILogger<LocationsController> _logger;
 
    
    public LocationsController(ILocationService locationService, ILogger<LocationsController> logger)
    {
        _locationService = locationService;
        _logger = logger;
    }

    [HttpPost("CreateLocation")]
    public async Task<ActionResult<Location>> CreateLocationAsync(Location location)
    {
       await _locationService.CreateLocationAsync(location);
        _logger.LogInformation("Created Location");
        return Ok(location);
    }

    [HttpGet("GetAllLocations")]

    public async Task<ActionResult<List<Location>>> GetAllLocationsAsync()
    {
        var list = await _locationService.GetAllLocationsAsync();
        _logger.LogInformation("Get all Locations");
        return Ok(list);
        
    }
}