using EventManager.LocationService;
using EventManager.Model;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Controllers;
[ApiController]
[Route("Location/")]
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

    [HttpGet("GetLocation/{id}")]
    public async Task<ActionResult<Location>> GetLocationAsync(int id)
    {
        var locationId = await _locationService.GetLocationByIdAsync(id);
        _logger.LogInformation("Get Location Information found");
        return Ok(locationId);
    }

    [HttpPut("UpdateLocation/{id}")]
    public async Task<ActionResult<Location>> UpdateLocationAsync(int id,[FromBody] Location location)
    {
        var updateLocation =  await _locationService.UpdateLocationAsync(id,location);
        _logger.LogInformation("Updated Location");
        return Ok(updateLocation);
    }

    [HttpDelete("DeleteLocation/{id}")]
    public async Task<ActionResult<Location>> DeleteLocationAsync(int id)
    {
       await _locationService.DeleteLocationAsync(id);
        _logger.LogInformation("Deleted Location");
        return Ok();
    }
}