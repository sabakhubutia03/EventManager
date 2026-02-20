namespace EventManager.Model;

public class Registration
{
    public int EventId { get; set; }
    public int AttendeeId { get; set; }
    public Attendee Attendee { get; set; } 
    
    public DateTime RegisteredAtUtc  { get; set; }
}