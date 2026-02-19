using EventManager.Model;

namespace EventManager.LocationService;

public interface IEventServices
{
   
    Task<Event> CreateEventAsync(Event _event);
    Task<Event> GetEventIdAsync(int id);
    Task<List<Event>> GetAllEventsAsync();
    Task<Event> updateEventAsync(int id,Event _event);
    Task DeleteEventAsync(int id);
    
}