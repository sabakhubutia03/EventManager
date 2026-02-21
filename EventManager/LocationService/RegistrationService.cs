using EventManager.EventManagerDbContext;
using EventManager.Model;
using Microsoft.EntityFrameworkCore;

namespace EventManager.LocationService;

public class RegistrationService : IRegistrationService
{
    public readonly EventMenagerDb _Db;
    public readonly ILogger<RegistrationService> _logger;

    public RegistrationService(EventMenagerDb db, ILogger<RegistrationService> logger)
    {
        _Db = db;
        _logger = logger;
    }
    
    
    public  async Task<Registration> RegisterAttendeeToEvent(int eventId, int attendeeId)
    {
        var events =  await _Db.Events.FindAsync(eventId);
        if (events == null)
        {
            _logger.LogError("Event not found");
            throw new ApiException(
                "Notfound",
                "Event not found",
                404,
                "Event not found",
                "Validation error");
        } 
        
        var attend = await _Db.Attendees.FindAsync(attendeeId);
        if (attend == null)
        {
            _logger.LogError("Event not found");
            throw new ApiException(
                "Notfound",
                "Event not found",
                404,
                "Event not found",
                "Validation error");
        }
        
        var alreadyRegistered = await _Db.Registrations
            .AnyAsync(x => x.EventId == eventId && x.AttendeeId == attendeeId);
        if (alreadyRegistered)
        {
            _logger.LogError("Attendee is already registered for this event");
            throw new ApiException(
                "Conflict",
                "Attendee already registered",
                409,
                "Attendee already registered",
                "Validation error");
        }

        var newRegistration = new Registration()
        {
            EventId = eventId,
            AttendeeId = attendeeId,
            RegisteredAtUtc = DateTime.UtcNow,
        };
        
        await _Db.Registrations.AddAsync(newRegistration);
        await _Db.SaveChangesAsync();
        return newRegistration;
    }

    public async Task RemoveAttendeeFromEvent(int eventId, int attendeeId)
    {
        var registation = await _Db.Registrations.
            FirstOrDefaultAsync(x => x.EventId == eventId && x.AttendeeId == attendeeId);

        if (registation == null)
        {
            _logger.LogError("Attendee is not registered for this event");
            throw new ApiException(
                "BadRequest",
                "Attendee is not registered for this event",
                400,
                "Attendee not registered for this event",
                "Validation error");
        }

         _Db.Registrations.Remove(registation);
        await _Db.SaveChangesAsync();
    }

    public  async Task<List<Attendee>> GetEventAttendees(int eventId)
    {
        var attend = await _Db.Registrations.
            Where(r => r.EventId == eventId).
            Select(r => r.Attendee).
            ToListAsync();
        return attend;
    }

    public async Task<List<Event>> GetAttendeeEvents(int attendeeId)
    {
        var events = await _Db.Registrations
            .Where(r => r.AttendeeId == attendeeId)
            .Select(r => r.Event)    
            .ToListAsync();

        return events;
    }
}