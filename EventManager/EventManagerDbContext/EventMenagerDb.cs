using EventManager.Model;
using Microsoft.EntityFrameworkCore;

namespace EventManager.EventManagerDbContext;

public class EventMenagerDb : DbContext
{
    public EventMenagerDb(DbContextOptions<EventMenagerDb> options) : base(options) {}
    
    public DbSet<Location> Locations { get; set; }
}