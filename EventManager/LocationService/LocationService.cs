using EventManager.EventManagerDbContext;
using EventManager.Model;
using Microsoft.EntityFrameworkCore;

namespace EventManager.LocationService;

public class LocationService : ILocationService
{
    public readonly EventMenagerDb _eventMenagerDb;
    public readonly ILogger<LocationService> _logger;

    public LocationService(EventMenagerDb eventMenagerDb, ILogger<LocationService> logger)
    {
        _eventMenagerDb = eventMenagerDb;
        _logger = logger;
    }
    public  async Task CreateLocationAsync(Location location)
    {
        if (location == null)
        {
            _logger.LogWarning("Location is null");
            throw new ApiException(
                "BadRequest" ,
                "Location is null empty",
                400,
                "Location is null",
                "Location is empty");
        }
        
        _eventMenagerDb.Locations.Add(location);
        await _eventMenagerDb.SaveChangesAsync();
    }

    public async Task<List<Location>> GetAllLocationsAsync()
    {
        var locationList =  await _eventMenagerDb.Locations.ToListAsync();

        if (locationList.Count == 0)
        {
            _logger.LogWarning("No locations found");
            throw new ApiException(
                "No locations found",
                "No locations found",
                404,
                "No locations found",
                "No locations found");
        }

        return locationList;
    }

    public Task<Location> GetLocationByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateLocationAsync(Location location)
    {
        throw new NotImplementedException();
    }
    

    public Task DeleteLocationAsync(Location location)
    {
        throw new NotImplementedException();
        
    }
}