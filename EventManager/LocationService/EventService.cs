using EventManager.EventManagerDbContext;
using EventManager.Model;
using Microsoft.EntityFrameworkCore;

namespace EventManager.LocationService;

public class EventService : IEventServices
{
    public readonly EventMenagerDb _db;
    public readonly ILogger<EventService> _logger;

    public EventService(EventMenagerDb db, ILogger<EventService> logger)
    {
        _db = db;
        _logger = logger;
    }
    public async Task<Event> CreateEventAsync(Event _event)
    {
        if (_event.EndDate <= _event.StartDate)
        {
            _logger.LogWarning("EndDate must be after StartDate");
           throw new Exception("EndDate must be after StartDate");
        }
        var location =  await _db.Events.
            AnyAsync(i => i.Id == _event.LocationId);
        if (!location)
        {
            _logger.LogWarning("Location does not exist");
            throw new ApiException(
                "NotFound",
                "No location found",
                404,
                "Event not found",
                "Location does not exist");
        }
        
        _db.Events.Add(_event);
        await _db.SaveChangesAsync();
        return _event;
    }

    public Task<Event> GetEventIdAsync(int id)
    {
        return _db.Events.
            Include(i => i.Location)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public  async Task<Event> updateEventAsync(int id, Event _event)
    {
       var updateEvent = await _db.Events.
           FirstOrDefaultAsync(i => i.Id == id);
       if (updateEvent == null)
       {
           _logger.LogWarning("No location found");
           throw new ApiException(
               "NotFound",
               "No location found",
               404,
               "No location found",
               "No location found");
       }
       
      
       updateEvent.Title = _event.Title;
       updateEvent.Description = _event.Description;
       updateEvent.StartDate = _event.StartDate;
       updateEvent.EndDate = _event.EndDate;
       updateEvent.LocationId = _event.LocationId;
       
       await _db.SaveChangesAsync();
       return _event;
    }

    public async Task DeleteEventAsync(int id)
    {
        var delete = await _db.Events.
            FirstOrDefaultAsync(d => d.Id == id);
        if (delete == null)
        {
            _logger.LogWarning("Deleted Event {Id} not Found" , id);
            throw new ApiException(
                "NotFound",
                "No Event found",
                404,
                "No Event found",
                "No Event found");
        }

        _db.Events.Remove(delete);
        _db.SaveChanges();

    }
}