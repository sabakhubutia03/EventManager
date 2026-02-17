using EventManager.Model;

namespace EventManager.LocationService;

public interface ILocationService
{
    Task CreateLocationAsync(Location location);
    
    Task<List<Location>> GetAllLocationsAsync();
    Task<Location> GetLocationByIdAsync(int id);
    
    Task<Location> UpdateLocationAsync(int id,Location location);
    
    
    Task DeleteLocationAsync(int id);
}