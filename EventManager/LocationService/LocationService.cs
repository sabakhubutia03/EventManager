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
    public async Task CreateLocationAsync(Location location)
    {
        
        if (string.IsNullOrWhiteSpace(location.Name) ||
            string.IsNullOrWhiteSpace(location.City))
        {
            _logger.LogWarning("Event title is empty");
            throw new ApiException(
                "BadRequest",
                "Event title cannot be empty",
                400,
                "Title is required",
                "Validation error");
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

    public async Task<Location> GetLocationByIdAsync(int id)
    {
        var  location =  await _eventMenagerDb.Locations.FirstOrDefaultAsync(l => l.Id == id);
        if (location == null)
        {
            _logger.LogWarning("No location found  Id - {Id}" , id);
            throw new ApiException(
                "NotFound",
                "No location found",
                404,
                "No location found",
                "No location found");
        }
        return location;
    }

    public async Task<Location> UpdateLocationAsync(int id,Location location)
    {
        var updatedLocation = await _eventMenagerDb.Locations.
            FirstOrDefaultAsync(i => i.Id == id );
        if (updatedLocation == null)
        {
            _logger.LogWarning("No location found Id - {Id}", id);
            throw new ApiException(
                "NotFound",
                "No location found",
                404,
                "No location found",
                "No location found");
        }
        
    // _eventMenagerDb.Entry(updatedLocation).CurrentValues.SetValues(location);
    // updatedLocation.Id = id;
    
    updatedLocation.Name = location.Name;
    updatedLocation.City = location.City;
    updatedLocation.Details = location.Details;
        
        await _eventMenagerDb.SaveChangesAsync(); 
        return updatedLocation;
    }
    

    public async Task DeleteLocationAsync(int id)
    {
        var deletedLocation = await _eventMenagerDb.Locations.
            FirstOrDefaultAsync(d => d.Id == id);
        if (deletedLocation == null)
        {
            _logger.LogWarning("Deleted location {Id} not Found" , id);
            throw new ApiException(
                "NotFound",
                "No location found",
                404,
                "No location found",
                "No location found");
        }
        _eventMenagerDb.Locations.Remove(deletedLocation);
        await _eventMenagerDb.SaveChangesAsync();
    }
}