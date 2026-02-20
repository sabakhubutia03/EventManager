using EventManager.LocationService;

namespace EventManager.Model;

public class Attendee
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    
    public List<Registration> Registrations { get; set; }
}