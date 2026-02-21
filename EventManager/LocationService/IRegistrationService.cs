using EventManager.Model;

namespace EventManager.LocationService;

public interface IRegistrationService
{
    Task<Registration> RegisterAttendeeToEvent(int eventId, int attendeeId);

    Task RemoveAttendeeFromEvent(int eventId, int attendeeId);

    Task<List<Attendee>> GetEventAttendees(int eventId);

    Task<List<Event>> GetAttendeeEvents(int attendeeId);
}