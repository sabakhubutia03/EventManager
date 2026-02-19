using EventManager.LocationService;
using EventManager.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Controllers;

[ApiController]
[Route("Event/")]
public class EventController : ControllerBase
{
    public readonly IEventServices _eventServices;
    public readonly ILogger<EventController> _logger;

    public EventController(IEventServices eventServices, ILogger<EventController> logger)
    {
        _eventServices = eventServices;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
        var eventIdGet = await _eventServices.GetEventIdAsync(id);
        _logger.LogInformation("Event Information found");
        return Ok(eventIdGet);

    }

    [HttpGet("EventAll")]

    public async Task<ActionResult<List<Event>>> GetEventAll()
    {
     var getAllEvent=  await _eventServices.GetAllEventsAsync();
        _logger.LogInformation("Event Information found");
        return Ok(getAllEvent);
    }
    [HttpPost]
    public async Task<ActionResult<Event>> PostEvent(Event _event)
    {
     var creatEvent =  await _eventServices.CreateEventAsync(_event);
        _logger.LogInformation("Event created");
        return Ok(creatEvent);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Event>> PutEvent(int id, Event _event)
    {
        var udateteEvent = await _eventServices.
            updateEventAsync(id, _event);
        _logger.LogInformation("Event updated");
        return Ok(udateteEvent);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Event>> DeleteEvent(int id)
    {
        await _eventServices.DeleteEventAsync(id);
        _logger.LogInformation("Event deleted");
        return Ok();
    }

}