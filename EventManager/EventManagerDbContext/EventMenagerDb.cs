using EventManager.Model;
using Microsoft.EntityFrameworkCore;

namespace EventManager.EventManagerDbContext;


public class EventMenagerDb : DbContext
{
    public EventMenagerDb(DbContextOptions<EventMenagerDb> options) : base(options) {}
    
    public DbSet<Location> Locations { get; set; }
    public DbSet<Event> Events { get; set; } 
    
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registration>()
            .HasKey(r => new { r.EventId, r.AttendeeId });
        
        modelBuilder.Entity<Attendee>().ToTable("Attendee");
        modelBuilder.Entity<Event>().ToTable("Events");
        modelBuilder.Entity<Registration>().ToTable("Registration");

    }
}