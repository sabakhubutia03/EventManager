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
            throw new ApiException(
                "BadRequest",
                "Invalid date range",
                400,
                "EndDate must be after StartDate",
                "Validation error");

        }
    
        
        var location = await _db.Locations.
            AnyAsync(i => i.Id == _event.LocationId);
        if (!location)
        {
            _logger.LogWarning("Location does not exist");
            throw new ApiException(
                "NotFound",
                "Location not found",
                404,
                "Location does not exist",
                "Validation error");
        }
        
        _db.Events.Add(_event);
        await _db.SaveChangesAsync();
        return _event;
    }

    public async Task<Event> GetEventIdAsync(int id)
    {
      var getEventId =  await _db.Events.
          FirstOrDefaultAsync(i => i.Id == id);
      if (getEventId == null)
      {
          _logger.LogWarning("Event  Id {Id} not found" , id);
          throw new ApiException(
              "NotFound",
              "No event",
              404,
              "Event not found",
              "Event not found");
      }

      return getEventId;
    }

    public async Task<List<Event>> GetAllEventsAsync()
    {
        var getAllEvent =  await  _db.Events.ToListAsync();
        if (getAllEvent == null)
        {
            _logger.LogWarning("Events not found");
            throw new ApiException(
                "NotFound",
                "No events",
                404,
                "Events not found",
                "Events not found");
        }
        
        return getAllEvent;
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
        await _db.SaveChangesAsync();

    }
}