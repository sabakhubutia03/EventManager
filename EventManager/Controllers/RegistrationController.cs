using EventManager.LocationService;
using EventManager.Model;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Controllers;

[ApiController]
[Route("Attendee-registration")]
public class RegistrationController : ControllerBase
{
    public readonly IRegistrationService _registrationService;
    public readonly ILogger<RegistrationController> _logger;

    public RegistrationController(IRegistrationService registrationService, ILogger<RegistrationController> logger)
    {
        _registrationService = registrationService;
        _logger = logger;
    }

    [HttpPost("Registration/{eventId}/{attendeeId}")]
    public async Task<ActionResult> RegisterAttendeeToEvent(int eventId, int attendeeId)
    {
        var registation = await _registrationService
            .RegisterAttendeeToEvent(eventId, attendeeId);
        _logger.LogInformation("Registered attendee");
        return Ok(registation);
    }

    [HttpDelete("DeleteAttendee")]
    public  async Task<ActionResult> DeleteAttendee(int eventId, int attendeeId)
    {
        await _registrationService.RemoveAttendeeFromEvent(eventId,attendeeId);
        _logger.LogInformation("Removed attendee");
        return Ok();
    }

    [HttpGet("GetEventAttendees/{eventId}")]
    public async Task<ActionResult<List<Attendee>>> GetEventAttendees(int eventId)
    {
        var attends = await  _registrationService.GetEventAttendees(eventId);
        _logger.LogInformation("Getting events attendees");
        return Ok(attends);
    }

    [HttpGet("GetAttendees/{attendeeId}")]

    public async Task<ActionResult<List<Event>>> GetAttendeeEvents(int attendeeId)
    {
        var events = await _registrationService.GetAttendeeEvents(attendeeId);
        _logger.LogInformation("Getting events attendees");
        return Ok(events);
    }
    
        
}