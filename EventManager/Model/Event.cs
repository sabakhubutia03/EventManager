using System.Text.Json.Serialization;

namespace EventManager.Model;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int LocationId { get; set; }
    
    [JsonIgnore]
    public Location? Location { get; set; }
}